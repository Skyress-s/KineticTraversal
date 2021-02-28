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

    public enum WallrunStates
    {
        walldetect,
        wallrun,
        exit,
        cooldown
    }

    public WallrunStates currentWallrunState;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && wallrunning == true)
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
                DetectWall();
                break;
            case (WallrunStates.wallrun):
                Wallrunn();
                break;
            case (WallrunStates.exit):
                //dad
                break;
            case (WallrunStates.cooldown):
                CooldownFromWallrun();
                break;
        }

        //old method
    }

    void DetectWall()
    {
        //sets the wallrun variable for animation to false
        wallrunning = false;

        //checks left and right side of player
        RaycastHit hitRight = WallDetectionV2(true);
        RaycastHit hitLeft = WallDetectionV2(false);
        RaycastHit hit = new RaycastHit(); //the primary hit to use further on :^)

        if (hitLeft.collider != null)
        {
            hit = hitLeft;
            sideDeterming = -1;
            //Debug.Log("left");
        }
        else if (hitRight.collider != null)
        {
            hit = hitRight;
            sideDeterming = 1;
            //Debug.Log("right");
        }

        if (hit.collider != null)
        {
            globalHit = hit;
            currentWallrunState = WallrunStates.wallrun;
        }
        else
        {
            var emptyHit = new RaycastHit();
            globalHit = emptyHit;
        }

    }
    void Wallrunn()
    {
        // if player is on the ground, do not wallrun, and return to walldetect
        if (IIC.grounded._isgrounded)
        {
            currentWallrunState = WallrunStates.cooldown;
            detachTime = Time.time;
            return;
        }

        //shoots out a new ray -> in hit.normal dir to check i close enough to wall
        var hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -globalHit.normal, out hit, rayD))
        {
            //the object it hits is the new global hit
            globalHit = hit;

            //if speed is to low, do not wallrun
            var heyAgusta = rb.velocity;
            heyAgusta.y = 0f;
            if (heyAgusta.magnitude > minimumSpeed) // what todo if wallrun requierments are met
            {
                Wallrun(globalHit);
                StickToWall(globalHit);
                TempDisableAirControl(false);

                //sets the wallrunning bool to active (gives info if wallrunning or not)
                wallrunning = true;
            }
        }
        else
        {
            currentWallrunState = WallrunStates.cooldown;
            detachTime = Time.time;
        }
    }
    void JumpOffWall()
    {

    }
    void CooldownFromWallrun()
    {
        wallrunning = false;
        lerpYForce = 0f;
        if (Time.time > detachTime + 0.3f)
        {
            TempDisableAirControl(true);
        }

        if (Time.time > detachTime + 0.4f)
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
        Debug.Log("jump");
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
        vAlongWall = vAlongWall.normalized * sideDeterming;


        Debug.DrawRay(transform.position, vAlongWall * 3, Color.green, Time.fixedDeltaTime);

        if (rb.velocity.magnitude < speedBoostCap)
        {
            rb.AddForce(vAlongWall * BoostAcceleration, ForceMode.VelocityChange);
        }

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
        return sideDeterming;
    }
}
