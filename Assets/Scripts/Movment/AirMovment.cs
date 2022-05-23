using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirMovment : MonoBehaviour
{
    [Header("Bernt :)")]
    public string Bernt;
    
    [Header("Dependencies")]
    private Rigidbody rb;

    public InputInfoCenter IIC;
    public GrapplingHookStates _GrapplingHookStates;
    private Transform cameraT;

    [Header("Range")]
    [Range(0f, 180f)]
    public float Bernt1, Bernt2;

    [Header("Low speed add force")]
    [Space][Tooltip("is speed is under this value, will add a weak force to move the player in air")]
    public float initalizeAddSpeed;
    public float lowSpeedAddForce;
    [Tooltip("how much time player can be in the air before this function wont run")]
    public float airTimeLimit;

    
    [Header("SideChangeDirection")]
    
    public float sideChangeAmoundt;
    [Header("AirBrake")]
    [SerializeField][Range(0f,10f)]
    private float airBrakeForce;

    public enum StrafeState { 
        notstrafing,
        sharpStrafe,
        BluntStrafe,
        AirBrake
    }

    public StrafeState currentStrafeState;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraT = Camera.main.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentStrafeState = StrafeState.notstrafing;
        
        // //guard clause
        // if (_GrapplingHookStates.currentState == GrapplingHookStates.GHStates.hooked) {
        //     return;
        // }
        
        // change the direction on the air -> air stafe system 2.0
        if (IIC.grounded._isgrounded == false && IIC.input.magnitude > 0.1f) // player has to be airborne and inputting somthing for this to work
        {
            //first gets the angle between velocity and input
            GetAngleDifference();


            if (GetAngleDifference() < Bernt1) // what todo if in range of Bernt1
            {
                //Debug.Log("airstrafing");
                Airstrafe();
                currentStrafeState = StrafeState.sharpStrafe;
            }
            else if (GetAngleDifference() < Bernt2) // what todo if in range of Bernt2
            {
                //Debug.Log("Change direction slowly");
                SideChangeDirection();
                currentStrafeState = StrafeState.BluntStrafe;
            }
            else // what todo if over range of Bernt2 (third range)
            {
                //Debug.Log("airbrake");
                AirBrake();
                currentStrafeState = StrafeState.AirBrake;
            }

            //add force if speed is low enough system
            var rby = rb.velocity;
            rby.y = 0f;
            if (rby.sqrMagnitude < initalizeAddSpeed * initalizeAddSpeed &&
                IIC.AirTime.airTime < airTimeLimit)
            {
                rb.AddForce(IIC.worldInput * lowSpeedAddForce, ForceMode.VelocityChange);
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
        var v = Vector3.RotateTowards(velocity, IIC.worldInput, Mathf.PI * (1f / 150f) * sideChangeAmoundt, 0f);
        v.y = rb.velocity.y;
        rb.velocity = v;
    }

    void SideChangePosition() //sligtly changes the position of the player in air to give a bit more control
    {
        var pos = transform.position;
        var newPos = pos + IIC.worldInput * 0.1f;
        transform.position = newPos;
    }

    void AirBrake() // makes the player lose momentum quickly in air
    {
        rb.AddForce(IIC.worldInput * airBrakeForce, ForceMode.VelocityChange);
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

    private void OnDisable()
    {
        currentStrafeState = StrafeState.notstrafing;
    }
}

