using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputInfoCenter : MonoBehaviour
{
    public PlayerControl controls;

    public IsGrounded grounded;


    public GrapplingHookStates grapplingHookStates;

    public Sliding infoSliding;

    public GroundMovment_ForceVer GroundMovment;

    public AirMovment _AirMovment;

    public AirDash _AirDash;

    public GameObject hook;

    public Vector2 input;

    public Vector3 worldInput;

    public AirTime AirTime;
    
    public WallrunWallDetect WallrunDetect;

    public Wallrunning Wallrunning;

    [Header("Unity New Input System")]

    public bool holdJump;

    public bool holdSprint;

    public bool holdLengthenRope;

    public bool holdShortenRope;

    public bool holdSlide;

    public bool holdFire;

    [Header("Player State Machine")]
    public Context Context;

    private void Awake()
    {
        controls = new PlayerControl();

        //implements hold Space/jump bool
        controls.Player.Jump.started += x => holdJump = true;
        controls.Player.Jump.canceled += x => holdJump = false;

        //implements hold shift/sprint bool
        controls.Player.Sprint.started += z => holdSprint = true;
        controls.Player.Sprint.canceled += z => holdSprint = false;

        //implements hold lengthen rope bool
        controls.Player.LengthenRope.started += y => holdLengthenRope = true;
        controls.Player.LengthenRope.canceled += y => holdLengthenRope = false;

        //implements hold shorten rope bool
        controls.Player.ShortenRope.started += a => holdShortenRope = true;
        controls.Player.ShortenRope.canceled += a => holdShortenRope = false;

        //implements hold slide bool
        controls.Player.Slide.started += b => holdSlide = true;
        controls.Player.Slide.canceled += b => holdSlide = false;

        controls.Player.Shoot.started += c => holdFire = true;
        controls.Player.Shoot.canceled += c => holdFire = false;
    }

    void Update()
    {
        //gets the input
        input = controls.Player.Movment.ReadValue<Vector2>();
        GetWordInput();
        

        //finds the time since last jump
    }

    private void GetWordInput()
    {
        //input.x = Input.GetAxisRaw("Horizontal");
        //input.y = Input.GetAxisRaw("Vertical");

        //calculates the worldinput with y=0

        var cameraTrans = Camera.main.transform;
        //defines camera forward and right with y = 0
        var cameraForward = cameraTrans.forward;
        cameraForward.y = 0f;
        cameraForward = cameraForward.normalized;

        var cameraRight = cameraTrans.right;
        cameraRight.y = 0f;
        cameraRight = cameraRight.normalized;

        var wi = cameraRight * input.x + cameraForward * input.y;
        worldInput = wi.normalized;
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
