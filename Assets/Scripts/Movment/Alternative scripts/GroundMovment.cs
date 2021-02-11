using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovment : MonoBehaviour
{
    public Rigidbody rb;

    public Transform cameraTrans;

    private Vector3 inputVector;

    public float walkSpeed;

    private IsGrounded grounded;

    [SerializeField]
    private float lerpdown;

    public float deacc_intensity;

    public float acc_intensity;

    public float sprintMultiplier;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        cameraTrans = Camera.main.transform;
        grounded = GetComponent<IsGrounded>();
        
    }

    // Update is called once per frame
    void Update()
    {


        if (grounded._isgrounded)
        {
            //stores the y value of the velocity
            var _ystored = rb.velocity.y;

            inputVector = new Vector3(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));

            //makes a little damping functionality
            if (inputVector.magnitude > 0) //increasig
            {
                lerpdown += acc_intensity * Time.deltaTime;
            }
            else if (inputVector.magnitude < 0.1f) //decreasing
            {
                lerpdown -= deacc_intensity * Time.deltaTime;
            }


            //Claps the lerpvalue
            lerpdown = Mathf.Clamp(lerpdown, 0, 1);

            // adds the multiplier if shift is pressed
            float multiplySpeed = 0; 
            if (Input.GetKey(KeyCode.LeftShift))
            {
                multiplySpeed = sprintMultiplier;
            }
            else
            {
                multiplySpeed = 1;
            }


            Vector3 combined = new Vector3(0,0,0);
            if (inputVector.magnitude > 0.5f) //acceleration mode
            {
                var forward = cameraTrans.forward;
                forward.y = 0;
                forward.Normalize();
                forward = forward * inputVector.x;


                var sideways = cameraTrans.right;
                sideways.y = 0;
                sideways.Normalize();
                sideways = sideways * inputVector.z;

                combined = (forward + sideways) * walkSpeed * multiplySpeed * lerpdown + new Vector3(0, _ystored, 0);
            }
            else if (true) //deacceleration mode
            {
                var dir = rb.velocity;
                dir.y = 0;
                dir.Normalize();

                combined = dir * walkSpeed * lerpdown + new Vector3(0, _ystored, 0);
            }

            

            rb.velocity = combined;

        }
    }
}
