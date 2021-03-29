using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallrunning : MonoBehaviour
{
    public float rayD;

    public Transform Camera;

    private Rigidbody rb;

    public float lerpYForce;

    private int sideDeterming; // 1 is right and -1 is left
    
    //for xz portion
    public float speedBoostCap;

    public float BoostAcceleration;


    //jump portion
    public float jumpForce, jumpUpwardsForce, minimumSpeed;

    public bool wallrunning;

    public RaycastHit globalHit;

    public InputInfoCenter IIC;

    public float detachTime;

    [Header("ColliderTrigger segment")]
    public ColliderTrigger ct;

    public enum WallrunStates
    {
        walldetect,
        buffer,
        wallrun,
        exit,
        cooldown
    }

    public WallrunStates currentWallrunState;
    
    [SerializeField]
    private float disableAircontrolTime;
    [SerializeField]
    private float disableWallrunTime;

    public WallrunWallDetect _WallrunWallDetect;

    [Header("BufferState")][SerializeField]
    private float bufferDuration;
    private float currentBufferDuration = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (IIC.controls.Player.Jump.triggered && wallrunning == true)
        {
            OnJump();
        }
    }
    void FixedUpdate()
    {
        // new method
        switch (currentWallrunState)
        {
            case (WallrunStates.walldetect):
                DetectWallState();
                break;
            case (WallrunStates.buffer):
                BufferState();
                break;
            case (WallrunStates.wallrun):
                WallrunState();
                break;
            case (WallrunStates.exit):
                //dad
                break;
            case (WallrunStates.cooldown):
                CooldownState();
                break;
        }
    }

   
    void DetectWallState()
    {
        #region
        ////sets the wallrun variable for animation to false
        //wallrunning = false;

        ////checks left and right side of player
        //RaycastHit hitRight = WallDetectionV2(true);
        //RaycastHit hitLeft = WallDetectionV2(false);
        //RaycastHit hit = new RaycastHit(); //the primary hit to use further on :^)

        //if (hitLeft.collider != null)
        //{
        //    hit = hitLeft;
        //    sideDeterming = -1;
        //    //Debug.Log("left");
        //}
        //else if (hitRight.collider != null)
        //{
        //    hit = hitRight;
        //    sideDeterming = 1;
        //    //Debug.Log("right");
        //}

        //if (hit.collider != null)
        //{
        //    globalHit = hit;
        //    currentWallrunState = WallrunStates.wallrun;
        //}
        //else
        //{
        //    var emptyHit = new RaycastHit();
        //    globalHit = emptyHit;
        //}
        #endregion

        if (_WallrunWallDetect.wallrunning)
        {
            //on enter 
            EnterBufferState();

        }

    }


    void EnterBufferState()
    {

        globalHit = _WallrunWallDetect.globalHit;
        currentBufferDuration = 0;
        //resumes to the bufferstate
        currentWallrunState = WallrunStates.buffer;
        wallrunning = true;
    }
    void BufferState()
    {
        currentBufferDuration += Time.deltaTime;
        if (currentBufferDuration > bufferDuration)
        {
            currentWallrunState = WallrunStates.wallrun;
        }

        Wallrun(globalHit);
        StickToWall(globalHit);
        TempDisableAirControl(false);


    }

    void WallrunState()
    {
        //// if player is on the ground, do not wallrun, and return to walldetect
        //if (IIC.grounded._isgrounded)
        //{
        //    currentWallrunState = WallrunStates.cooldown;
        //    detachTime = Time.time;
        //    return;
        //}

        //shoots out a new ray -> in hit.normal dir to check i close enough to wall
        var hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -globalHit.normal, out hit, rayD) || ct.isTriggerd == true)
        {
            //the object it hits is the new global hit
            globalHit = hit;

            //if speed is to low, do not wallrun
            var heyAgusta = rb.velocity;
            heyAgusta.y = 0f;
            //if (heyAgusta.magnitude > minimumSpeed) // what todo if wallrun requierments are met
            //{
                Wallrun(globalHit);
                StickToWall(globalHit);
                TempDisableAirControl(false);
<<<<<<< HEAD
            //}
=======
            }
>>>>>>> 2530c0655a5d19272e2570d7a60403da8165c159
        }
        else
        {
            currentWallrunState = WallrunStates.cooldown;
            detachTime = Time.time;
        }
    }
    
    void CooldownState()
    {
        wallrunning = false;
        lerpYForce = 0f;
        if (Time.time > detachTime + disableAircontrolTime) // how long to disable air control
        {
            TempDisableAirControl(true);
        }

        if (Time.time > detachTime + disableWallrunTime) // how long to disable wallrunning
        {
            currentWallrunState = WallrunStates.walldetect;
        }
    }

    /// <summary>
    /// Turn the airControl script on and off with a bool
    /// </summary>
    /// <param name="b">if b is true enable, if false, disable</param>
    void TempDisableAirControl(bool b)
    {
        gameObject.GetComponent<AirMovment>().enabled = b;
    }
    
    void StickToWall(RaycastHit hit)
    {
        
            rb.AddForce(-hit.normal * 1f, ForceMode.VelocityChange);
        
    }
    void OnJump()
    {
        //Debug.Log("walljump");
        wallrunning = false;
        var dir = Camera.transform.forward;
        var n = globalHit.normal;
        var c = dir + n;
        c = c.normalized;

        rb.AddForce(c * jumpForce + Vector3.up * jumpUpwardsForce, ForceMode.VelocityChange);

        //sets the current wallrunning state to WallrunCooldown
        detachTime = Time.time;
        currentWallrunState = WallrunStates.cooldown;
    }

    void Wallrun(RaycastHit hit)
    {
        // the y portion of the wallrun
        //increases the y force
        lerpYForce += Time.fixedDeltaTime/2f;

        lerpYForce = Mathf.Clamp(lerpYForce, 0f, 1f);


        //cancels out the -y velocity
        var v = rb.velocity.y;
        if (v > 0) v = 0f;

        rb.AddForce(new Vector3(0, -v * lerpYForce, 0), ForceMode.VelocityChange);

        //the xz portion of the code
        Vector3 vAlongWall = Vector3.Cross(Vector3.up, hit.normal);
        vAlongWall = vAlongWall.normalized * DetermineSide();


        Debug.DrawRay(transform.position, vAlongWall * 6, Color.black, Time.fixedDeltaTime);
        if (rb.velocity.magnitude < speedBoostCap)
        {
            rb.AddForce(vAlongWall * BoostAcceleration, ForceMode.VelocityChange);
        }

        //Debug.DrawRay(transform.position, vAlongWall * 100f, Color.grey, Time.deltaTime);

    }

    /// <summary>
    /// Shoots out a ray to see if it hits something
    /// </summary>
    /// <param name="b">true shoots the ray to the right</param>
    /// <returns></returns>
    RaycastHit WallDetectionV2(bool b)
    {
        var right = 0;
        if (b == true) right = 1;
        else right = -1;

        var v = Camera.right;
        v.y = 0f;
        v = v.normalized;


        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, v * right, out hit, rayD))
        {
            return hit;
        }
        else
        {
            return hit;
        }
    }

    private void OnDrawGizmos()
    {
        //draws a ray for the walldetection
        Vector3 v = RemoveYComponent(Camera.right);
        v = v.normalized * rayD;
        Gizmos.DrawRay(transform.position, v);
        Gizmos.DrawRay(transform.position, -v);

    }

    private Vector3 RemoveYComponent(Vector3 v)
    {
        v = new Vector3(v.x, 0, v.z);
        return v;
    }

    public int DetermineSide()
    {
        int x;
        if (_WallrunWallDetect.isRightSide) x = 1;
        else x = -1;

        return x;
    }
}





