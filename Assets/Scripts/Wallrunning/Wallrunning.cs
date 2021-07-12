using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallrunning : MonoBehaviour
{
    public float rayD;

    public Transform Camera;

    private Rigidbody rb;

    public float lerpYForce;

    //for xz portion
    [Header("Horizontal speed")]
    public float MaxSpeed;

    public float speedBoost;

    //jump portion
    [Header("Jump")]
    public float jumpForce;
    [Tooltip("How hard to jump")]
    public float jumpUpwardsForce;

    [Tooltip("Maximum speed the player can have before not incresing speed when jumping")]
    /// <summary>
    /// maximum speed for giving a speed increase
    /// </summary>
    public float maximumSpeed;

    [Space]
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
        exit
    }

    public WallrunStates currentWallrunState;

    [SerializeField]
    private float disableAircontrolTime;

    public WallrunWallDetect _WallrunWallDetect;

    [Header("BufferState")]
    [SerializeField]
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

        if (_WallrunWallDetect.detected)
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
        wallrunning = true;
        currentWallrunState = WallrunStates.buffer;
    }
    void BufferState()
    {
        currentBufferDuration += Time.deltaTime;
        if (currentBufferDuration > bufferDuration)
        {
            currentWallrunState = WallrunStates.wallrun;
        }

        //updates globalHit to walldetect so bufferstate dosent boost player too much
        globalHit = _WallrunWallDetect.globalHit;

        Wallrun(globalHit);
        StickToWall(globalHit);
    }

    void WallrunState()
    {

        //shoots out a new ray -> in hit.normal dir to check i close enough to wall
        var hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -globalHit.normal, out hit, rayD) || ct.isTriggerd == true)
        {
            //Debug.DrawRay(transform.position, -globalHit.normal * rayD, Color.white);
            //the object it hits is the new global hit
            globalHit = hit;

            WallrunBoost();
            Wallrun(globalHit);
            StickToWall(globalHit);
        }
        else
        {
            Debug.Log("exitttt!!!");
            Exit();
        }
    }
    void StickToWall(RaycastHit hit)
    {

        rb.AddForce(-hit.normal * 1f, ForceMode.VelocityChange);

    }
    void OnJump()
    {
        void Walljump()
        {
            //Debug.Log("walljump");
            wallrunning = false;


            //getting some useful info/variables
            var v = rb.velocity;
            var vmag = v.magnitude;
            var speed_factor = 1f;
            var look = Camera.transform.forward;


            // calc the y amount of the jump
            var Y = Vector3.up * look.y*3f;
            Y = Y * 3f;
            Y += Vector3.up * 10f;


            // calc the xy amount of the jump
            var XY = new Vector3(look.x, 0f, look.z).normalized;

            XY += globalHit.normal * 0.2f;
            
            XY = XY.normalized;
                // adding a speed boost if speed is below a certion amount
            if (new Vector3(v.x, 0f, v.z).sqrMagnitude < maximumSpeed * maximumSpeed)
            {

                //Debug.Log(new Vector3(v.x, 0f, v.z).sqrMagnitude);
                speed_factor = 1.3f;
               

                //Debug.Log("did increase speed");
            }
            

            //adding them toghether

            Vector3 newV = XY * vmag * speed_factor; // adds the XZ component

            newV += Y;

            //Debug.Log(vmag);

            //Debug.Log("before jump" + rb.velocity.magnitude.ToString());

            rb.velocity = newV;

            //Debug.Log(rb.velocity.magnitude);

            Exit();
        }

        Walljump();
    }

    void Wallrun(RaycastHit hit)
    {
        // the y portion of the wallrun
        //increases the y force
        lerpYForce += Time.fixedDeltaTime / 2f;

        lerpYForce = Mathf.Clamp(lerpYForce, 0f, 1f);

        //cancels out the -y velocity
        var v = rb.velocity.y;
        if (v > 0) v = 0f;

        rb.AddForce(new Vector3(0, -v * lerpYForce, 0), ForceMode.VelocityChange);

    }

    void WallrunBoost()
    {
        //boost the player
        Vector3 vAlongWall = Vector3.Cross(Vector3.up, globalHit.normal);
        vAlongWall = vAlongWall.normalized * DetermineSide();

        Debug.DrawRay(transform.position, vAlongWall * 4f, Color.black, Time.fixedDeltaTime);
        if (rb.velocity.magnitude < MaxSpeed)
        {
            rb.AddForce(vAlongWall * speedBoost, ForceMode.VelocityChange);
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

    public void Exit()
    {
        wallrunning = false;
        globalHit = new RaycastHit();
        lerpYForce = 1f;
        currentWallrunState = WallrunStates.exit;
    }

    private void OnDisable()
    {
        Exit();
    }

    private void OnEnable()
    {
        //currentWallrunState = WallrunStates.walldetect;
        EnterBufferState();
    }
}

