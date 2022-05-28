using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Wallrunning : MonoBehaviour {

    [SerializeField] private Rigidbody rb;
    public WallDetect wallDetect;
    
   
    public FWallrunState WallrunState;

    public FWallrunConfig WallrunConfig;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable() {
        WallrunState.CurrentWallrunState = EWallrunState.wallrun;
        
    }
    private void OnDisable() {
        wallDetect.ELostWall.RemoveListener(OnLostWall);
        PlayerController.instance.Control.Player.Jump.performed -= Jump;
    }
    void FixedUpdate()
    {
        // new method
        WallrunState.stateTime += Time.fixedDeltaTime;
        switch (WallrunState.CurrentWallrunState)
        {
            case (EWallrunState.walldetect):
                DetectWallState();
                break;
            case (EWallrunState.buffer):
                StateBuffer();
                break;
            case (EWallrunState.wallrun):
                StateWallrun();
                break;
            case (EWallrunState.exit):
                //dad
                break;
        }
    }


    void DetectWallState() {
        if (WallrunState.bEnter) {
            WallrunState.bEnter = false;
        }
        
        
    }
    
    void StateBuffer() {
        if (WallrunState.bEnter) {
            WallrunState.bEnter = false;
        }
        
        
    }
    void StateWallrun() {
        if (WallrunState.bEnter) {
            WallrunState.bEnter = false;
            wallDetect.ELostWall.AddListener(OnLostWall);
            PlayerController.instance.Control.Player.Jump.performed += Jump;
        }

        if (WallrunState.bEnter) {
            WallrunState.CurrentWallrunState = EWallrunState.exit;
            return;
        }
        
        
        float yVelocity = rb.velocity.y;
        if (yVelocity < 0f) {
            rb.AddForce(-Vector3.up * yVelocity * WallrunConfig.AntiGravityForce, ForceMode.Acceleration);
        }
    }

    void OnLostWall() {
        WallrunState.CurrentWallrunState = EWallrunState.exit;
        WallrunState.bExit = true;
    }

    private void Jump(InputAction.CallbackContext obj) {
        float velocityMagnitude = rb.velocity.magnitude + WallrunConfig.JumpSpeedAddition;
        velocityMagnitude = Mathf.Clamp(velocityMagnitude, 0, WallrunConfig.JumpSpeedSpeedClamp);
        

        Vector3 newDirection = Camera.main.transform.forward;
        Vector3 newVelocity = newDirection * velocityMagnitude;
        rb.velocity = newVelocity;
        WallrunState.bExit = true;
        
        // rb.AddForce(Camera.main.transform.forward * WallrunConfig.JumpForce, ForceMode.VelocityChange);
    }

    [Serializable]
    public class FWallrunState {

        private EWallrunState currentWallrunState = EWallrunState.walldetect;
        public EWallrunState CurrentWallrunState {
            get => currentWallrunState;
            set {
                currentWallrunState = value;
                stateTime = 0f;
                bEnter = true;
                bExit = false;
            }
        }
        
        public bool bEnter { get;  set; }
        public float stateTime { get;  set; }
        public bool bExit = false;
    }
    
    [Serializable]
    public struct FWallrunConfig {
        public FWallrunConfig(float jumpForce) {
            
            JumpSpeedAddition = 0;
            AntiGravityForce = 0;
            StickToWallForce = 0;
            JumpSpeedSpeedClamp = 0f;
        }
        
        public float JumpSpeedAddition;
        public float JumpSpeedSpeedClamp;
        public float AntiGravityForce;
        public float StickToWallForce;
    }
    
    public enum EWallrunState
    {
        walldetect,
        buffer,
        wallrun,
        exit
    }
}

