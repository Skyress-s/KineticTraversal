using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallrunning : MonoBehaviour
{
    public float rayD;

    public Transform Camera;

    private Rigidbody rb;

    public float lerpYForce;

    public bool activatedLastFrame;

    private int sideDeterming; // 1 is right and -1 is left
    
    //for xz portion
    public float speedBoostCap;

    public float BoostAcceleration;

    private bool deactivated;

    //jump portion
    public float jumpForce, jumpUpwardsForce, minimumSpeed;

    public bool wallrunning;

    public RaycastHit globalHit;

    public InputInfoCenter IIC;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        deactivated = false;
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
        if (deactivated == true)
        {
            return;
        }

        //casts the raycasts
        //WallDetection(true, cooldownBool);
        //WallDetection(false, cooldownBool);

        RaycastHit hitRight = WallDetectionV2(true);
        
        RaycastHit hitLeft = WallDetectionV2(false);

        RaycastHit hit = new RaycastHit(); //the primary hit to uses further on :^)

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
        }
        else
        {
            var emptyHit = new RaycastHit();
            globalHit =emptyHit;
        }

        //if hit is somthing and player is not grounded, porceed
        if (hit.collider != null && IIC.grounded._isgrounded == false)
        {
            //if speed is to low, do not wallrun
            var heyAgusta = rb.velocity;
            heyAgusta.y = 0f;
            if (heyAgusta.magnitude > minimumSpeed) // what todo if wallrun requierments are met
            {
                Wallrun(hit);
                StickToWall(hit);
                TempDisableAirControl(false);

                //sets the wallrunning bool to active (gives info if wallrunning or not)
                wallrunning = true;
            }
            
        }
        else if (activatedLastFrame == true) // what todo on exit of wallrun
        {
            OnExit();
            //Debug.Log("exit");
            TempDisableAirControl(true);
        }
        else // sets wallrunning bool to false (gives info if wallrunning or not)
        {
            wallrunning = false;
        }

        //Debug.Log(hit.collider.gameObject.GetHashCode());

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
        StartCoroutine(WallrunCooldown());
    }

    private IEnumerator WallrunCooldown()
    {
        deactivated = true;
        yield return new WaitForSeconds(0.3f);
        deactivated = false;
    }

    void OnExit()
    {
        activatedLastFrame = false;
        lerpYForce = 0f;
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
            //updates activatedLastFrame
            activatedLastFrame = true;

            return hit;
        }
        else
        {
            return hit;
        }
    }

    void WallDetection(bool b, bool cd)
    {
        if (cd == true) return;
      

        var right = 0;
        if (b == true) right = 1;
        else right = -1;

        var v = Camera.right;
        v.y = 0f;
        v = v.normalized;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, v * right, out hit, rayD))
        {
            //increases the y force
            lerpYForce += Time.fixedDeltaTime/2f;

            //cancelse out the y velocity
            rb.AddForce(new Vector3(0, -(rb.velocity.y) * lerpYForce, 0), ForceMode.VelocityChange);

            //updated bool activatedLastFrame
            activatedLastFrame = true;
        }
        else if (activatedLastFrame == true)
        {
            activatedLastFrame = false;
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
