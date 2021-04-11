using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    public enum SlidingMode
    {
        toggle,
        hold
    }

    public SlidingMode currentSlidingMode;

    public KeyCode SlidingKey;

    public InputInfoCenter IIC;

    public bool sliding;

    private CapsuleCollider cc;
    private float orgHeight;

    private Rigidbody rb;

    [Header("Change direction")]
    [Space] [SerializeField] [Tooltip("how fast the change in direction should happen")]
    private float slidingChangeVelocity;


    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CapsuleCollider>();
        orgHeight = cc.height;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentSlidingMode)
        {
            case SlidingMode.hold:
                HoldSlidingMode();
                break;
            case SlidingMode.toggle:
                ToggleSlidingMode();
                break;
        }
    }

    void ToggleSlidingMode()
    {
        if (IIC.controls.Player.Slide.triggered) //Input.GetKeyDown(SlidingKey)
        {
            if (sliding)
            {
                Stand();
            }
            else
            {
                Slide();
            }
        }
    }

    void HoldSlidingMode()
    {
        if (IIC.holdSlide) //Input.GetKey(SlidingKey)
        {
            Slide();
        }
        else
        {
            Stand();
        }
    }

    void Slide()
    {
        sliding = true;
        cc.height = 1f;
        cc.center = new Vector3(0f, 0.3f, 0f);
    }

    void Stand()
    {
        sliding = false;
        cc.height = orgHeight;
        cc.center = new Vector3(0f, 0, 0f);
    }


    private void FixedUpdate()
    {
        //chagne dir while sliding functionaility
        if (sliding && IIC.input.sqrMagnitude > 0.1f)
        {
            //Debug.Log("activated");

            //stores the y value for later use
            var yStored = rb.velocity.y;
            //creates a version of the velocity wit y=0
            var rby = rb.velocity;
            rby.y = 0f;

            var var = Vector3.RotateTowards(rby, IIC.worldInput, Mathf.Deg2Rad * slidingChangeVelocity, 0);
            
            var.y = yStored;
            rb.velocity = var;
        }
    }
}
