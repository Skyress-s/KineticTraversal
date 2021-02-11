using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovment_ForceVer : MonoBehaviour
{
    private Transform CameraTrans;

    private Rigidbody rb;

    public IsGrounded Grounded;
    
    //Input
    private float x, y;

    //relative movment to Camera
    float yMag, xMag;
    

    //Movment Values
    public float maxSpeed, sprintMultiplier, counterMovIntensity, speedCapIntensity;

    private float maxSpeedStored;

    public InputInfoCenter inputInfoCenter;


    // Start is called before the first frame update
    void Start()
    {
        CameraTrans = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        maxSpeedStored = maxSpeed;
    }

    void FixedUpdate()
    {
        //resets the input values each tick
        x = 0f;
        y = 0f;

        //Gets the Input
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if (Grounded._isgrounded)
        {
            if (inputInfoCenter.infoSliding.sliding == true)
            {
                return;
            }

            Movment();
            SpeedCap();
        }


    }
    void CounterMovment()
    {
        // if left/right or/and forward/backwards counter move it
        if (x > -0.5f && x < 0.5f){
            rb.AddForce(Time.deltaTime * -xMag * RemoveYComponent(CameraTrans.right) * counterMovIntensity,
                ForceMode.VelocityChange);
        }

        if (y > -0.5f && y < 0.5f){
            rb.AddForce(Time.deltaTime * -yMag * RemoveYComponent(CameraTrans.forward) * counterMovIntensity,
                ForceMode.VelocityChange);
        }
    }

    void SpeedCap()
    {
        var v = rb.velocity;
        v.y = 0f;
        if (v.magnitude > maxSpeedStored)
        {
            rb.AddForce(-v.normalized * speedCapIntensity, ForceMode.VelocityChange);
        }
    }

    void Movment()
    {
        //find relative x, y speed
        xMag = FindVelRelativeToLook().x;
        yMag = FindVelRelativeToLook().y;

        CounterMovment();

        //implements sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxSpeed = maxSpeedStored * sprintMultiplier;
        }
        else
        {
            maxSpeed = maxSpeedStored;
        }

        ////if maxspeed is reached, make input = 0
        //if (x > 0 && xMag > maxSpeed) x = 0;
        //if (x < 0 && xMag < -maxSpeed) x = 0;
        //if (y > 0 && yMag > maxSpeed) y = 0;
        //if (y < 0 && yMag < -maxSpeed) y = 0;

        //finds the max speed vector relative to camera direction
        var newVel = new Vector2(x, y);
        newVel.Normalize();
        newVel = newVel * maxSpeed;
        //Debug.Log(newVel);

        float floatMargin = 0.05f;

        if (y > floatMargin)
        {
            if (yMag > newVel.y) y = 0;
            
        }
        if (y < -floatMargin)
        {
            if (yMag < newVel.y) y = 0;
        }

        if (x > floatMargin)
        {
            if (xMag > newVel.x) x = 0;
        }
        if (x < -floatMargin)
        {
            if (xMag < newVel.x) x = 0;
        }



        // Matches input to direction of looking
        var xVec = CameraTrans.right * x;
        var yVec = CameraTrans.forward * y;

        var combinedVec = xVec + yVec;
        combinedVec.y = 0f;
        combinedVec.Normalize();
        //Debug.Log(combinedVec);

       
        //applies the force
        rb.AddForce(combinedVec * Time.deltaTime * 100f,
            ForceMode.VelocityChange);
    }


    Vector3 RemoveYComponent(Vector3 vect)
    {
        var var = new Vector3(vect.x, 0, vect.z);
        return var;
    }

    public Vector2 FindVelRelativeToLook()
    {
        Vector3 vel = RemoveYComponent(rb.velocity);
        Vector3 lookForward = RemoveYComponent(CameraTrans.forward);
        Vector3 lookRight = RemoveYComponent(CameraTrans.right);


        float angleX = Vector3.Angle(lookRight, vel.normalized);

        float relXVel = vel.magnitude * Mathf.Cos(angleX * Mathf.Deg2Rad);

        float angleY = Vector3.Angle(lookForward, vel.normalized);

        float relYVel = vel.magnitude * Mathf.Cos(angleY * Mathf.Deg2Rad);

        //Debug.Log(new Vector2(relXVel, relYVel));
        return (new Vector2(relXVel, relYVel));

    }
}
