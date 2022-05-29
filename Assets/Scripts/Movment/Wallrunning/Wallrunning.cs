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
        WallrunState.Reset();
        
    }
    private void OnDisable() {
        wallDetect.ELostWall.RemoveListener(OnLostWall);
        PlayerController.instance.Control.Player.Jump.performed -= Jump;
        rb.useGravity = true;
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
            rb.useGravity = false;
        }
        
        //guard clause
        if (WallrunState.bExit) {
            return;
        }

        //antiWallrun
        float yVelocity = rb.velocity.y;
        if (yVelocity < 0f) {

            float UpVectorMul = WallrunState.stateTime / 0.7f;
            UpVectorMul = UpVectorMul < 1f ? 1f : UpVectorMul; // min clamps
            
            rb.AddForce(-Vector3.up * yVelocity * WallrunConfig.AntiGravityForce * UpVectorMul, ForceMode.Acceleration);
        }
        
        //wall stick
        rb.AddForce(-wallDetect.DetectState.Direction * WallrunConfig.StickToWallForce, ForceMode.Acceleration);
        
        
    }

    void OnLostWall() {
        WallrunState.CurrentWallrunState = EWallrunState.exit;
        WallrunState.bExit = true;
    }

    private void Jump(InputAction.CallbackContext obj) {
        float velocityMagnitude = rb.velocity.magnitude + WallrunConfig.JumpSpeedAddition;
        velocityMagnitude = Mathf.Clamp(velocityMagnitude, 0, WallrunConfig.JumpSpeedSpeedClamp);
        

        //sets new vel
        Vector3 newDirection = Camera.main.transform.forward;
        Vector3 newVelocity = newDirection * velocityMagnitude;
        rb.velocity = newVelocity;
        WallrunState.bExit = true;
        rb.useGravity = true;
        
    }

    [Serializable]
    public class FWallrunState {

        private EWallrunState currentWallrunState = EWallrunState.wallrun;
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


        public void Reset() {
            currentWallrunState = EWallrunState.wallrun;
            bEnter = true;
            stateTime = 0f;
            bExit = false;
        }
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
    
    [Serializable]
    public enum EWallrunState
    {
        walldetect,
        buffer,
        wallrun,
        exit
    }
}

