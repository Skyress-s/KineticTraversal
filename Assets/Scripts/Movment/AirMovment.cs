using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirMovment : MonoBehaviour
{
    public string Bernt;
    private Rigidbody rb;

    public InputInfoCenter IIC;

    private Transform cameraT;

    [Range(0f, 180f)]
    public float Bernt1, Bernt2;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraT = Camera.main.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IIC.grounded._isgrounded == false && IIC.input.magnitude > 0.1f) // player has to be airborne and inputting somthing for this to work
        {
            //first gets the angle between velocity and input
            GetAngleDifference();

            if (GetAngleDifference() < Bernt1) // what todo if in range of Bernt1
            {
                Debug.Log("airstrafing");
                Airstrafe();
            }
            else if (GetAngleDifference() < Bernt2) // what todo if in range of Bernt2
            {
                Debug.Log("Change direction slowly");
                SideChangeDirection();
            }
            else // what todo if over range of Bernt2 (third range)
            {
                Debug.Log("airbrake");
                AirBrake();
            }
        }
    }

    void Airstrafe() // makes the player airstrafe
    {

        //defines the input vector
        var wi = IIC.worldInput;
        //rb.AddForce(v * 10f * Time.deltaTime, ForceMode.VelocityChange);

        var velocity = rb.velocity;
        velocity.y = 0f;

        Vector3 v = wi * velocity.magnitude;
        v.y = rb.velocity.y;
        rb.velocity = v;
    }

    void SideChangeDirection()  // makes the player slowly change direction towards the worldinput vector
    {
        var velocity = rb.velocity;
        velocity.y = 0f;
        var v = Vector3.RotateTowards(velocity, IIC.worldInput, (Mathf.PI /150), 0f);
        v.y = rb.velocity.y;
        rb.velocity = v;
    }

    void AirBrake() // makes the player lose momentum quickly in air
    {
        rb.AddForce(IIC.worldInput * 1f, ForceMode.VelocityChange);
    }

    float GetAngleDifference()
    {
        //func gets the angle difference between the velocity and the input of the player
        //defines the velocity with y = 0
        var velocity = rb.velocity;
        velocity.y = 0f;
        velocity = velocity.normalized;

        //defines two camera directions with y = 0
        Vector3 cameraForward = cameraT.forward;
        cameraForward.y = 0f;

        Vector3 cameraRight = cameraT.right;
        cameraRight.y = 0f;
        
        //gets the input direction with y = 0
        var input = new Vector3(0, 0, 0);
        input = cameraForward * IIC.input.y;
        input = input + cameraRight * IIC.input.x;
        input = input.normalized;

        var signedAngle = Vector3.SignedAngle(velocity, input, Vector3.up);
        var angle = Vector3.Angle(velocity, input);

        //Debug.Log(signedAngle);
        return angle;
    }

    void SslowDown()
    {
        if (Input.GetKey(KeyCode.S))
        {
            var v = rb.velocity;
            v.y = 0f;
            //v = v.normalized;


            rb.AddForce(-v.normalized * v.magnitude * 5f * Time.deltaTime, ForceMode.VelocityChange);
        }

    }
}
