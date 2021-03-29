using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    private PlayerControl controls;

    //implementet sciptable object for jumping
    public TestScriptableObject TSO;

    private Rigidbody rb;

    private IsGrounded _grounded;

    public bool hoverJumpPhase;

    private float airTime;

    public InputInfoCenter IIC;
    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerControl();
        rb = GetComponent<Rigidbody>();
        _grounded = GetComponent<IsGrounded>();
    }

    // Update is called once per frame
    void Update()
    {
        //manages the airTime Value
        if (_grounded._isgrounded) airTime = 0;
        else airTime += Time.deltaTime;
        //Debug.Log(airTime);

        // Check if jump is "all systems go"
        if (controls.Player.Jump.triggered && _grounded._isgrounded == true)
        {
            DoJump();
        }

        //add coyoteJump functionality
        if (controls.Player.Jump.triggered && _grounded._isgrounded == false && airTime < TSO.coyoteMargin)
        {
            //DoJump();
        }

    }

    private void FixedUpdate()
    {


        //if holding down space, push the player higher up
        if (IIC.holdJump && hoverJumpPhase == true)
        {
            rb.AddForce(new Vector3(0, TSO.holdSpaceForce, 0), ForceMode.VelocityChange);
        }


        //disables hover jump phase if velociy.y < 0
        if (rb.velocity.y < -1f)
        {
            hoverJumpPhase = false;
        }
        if (IIC.WallrunDetect.wallrunning == true) // if wallrunning disable hoverun phase
        {
            hoverJumpPhase = false;
        }


        /*//pulls the player down when its goin' down
        if (rb.velocity.y < 0)
        {
            rb.AddForce(new Vector3(0, -downwardsForce, 0), ForceMode.VelocityChange);
        }*/
        
    }

    //jump func
    void DoJump()
    {
        //resets the velocity in y dir
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //jumps
        rb.AddForce(new Vector3(0, TSO.jumpForce, 0), ForceMode.VelocityChange);

        //caps the y velocity the player can have in inital jump
        if (rb.velocity.y > TSO.jumpForce)
        {
            Debug.Log("did slow down");
            Vector3 v = new Vector3(rb.velocity.x, TSO.jumpForce, rb.velocity.z);
            rb.velocity = v;
        }

        StartCoroutine(DisableGroundmovment()); //activates the disable groundmovment so it dosent mess with the speed while jump
                                    
        //activates the hoverjump phase
        hoverJumpPhase = true;

        //sets the airtime to somthing higher than coyoteMargin
        airTime = TSO.coyoteMargin + 1f;
    }

    //makes a corutine that disables movment for a spit second to that the jumping works properly
    public IEnumerator DisableGroundmovment()
    {
        var b = GetComponent<GroundMovment_ForceVer>();

        if (b.enabled == true)
        {
        b.enabled = false;
        yield return new WaitForSeconds(0.1f);
        b.enabled = true;
        airTime = TSO.coyoteMargin + 1f;  //sets the airtime to somthing higher than coyote margin

        }

    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }


}
