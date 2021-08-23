using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    [Tooltip("Sliding script chagnes the transfrom of the camera target to give the illusion that the player is sliding")]
    public Transform CameraTarget;
    private Vector3 CameraOrgPos;



    public enum SlidingMode
    {
        toggle,
        hold
    }

    [Header("Sliding info")]
    public SlidingMode currentSlidingMode;

    public KeyCode SlidingKey;

    public InputInfoCenter IIC;

    public bool sliding;

    private CapsuleCollider cc;
    private float orgHeight;

    private Rigidbody rb;

    [SerializeField]
    private float BeginSlideSpeedIncrease;
    [SerializeField]
    private float speedIncreaseCap;

    [Header("Change direction")]
    [Space] [SerializeField] [Tooltip("how fast the change in direction should happen")]
    private float slidingChangeVelocity;


    // Start is called before the first frame update
    void Start()
    {
        CameraOrgPos = CameraTarget.localPosition;

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
        void SpeedBoost()
        {
            float sqrmagv = rb.velocity.sqrMagnitude;
            if (sqrmagv < speedIncreaseCap * speedIncreaseCap && sqrmagv > 0.5f*0.5f)
            {
                rb.velocity += rb.velocity.normalized * BeginSlideSpeedIncrease;
            }
        }

        SpeedBoost();

        sliding = true;
        cc.height = 1f;
        cc.center = new Vector3(0f, -orgHeight/4f, 0f);

        //moves the camera target

        CameraTarget.localPosition = CameraOrgPos - new Vector3(0f, 1.2f, 0f);
    }

    void Stand()
    {
        sliding = false;
        cc.height = orgHeight;
        cc.center = new Vector3(0f, 0, 0f);

        //moves the camera target

        CameraTarget.localPosition = CameraOrgPos;

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

    private void OnDisable()
    {
        Stand();
    }
}
