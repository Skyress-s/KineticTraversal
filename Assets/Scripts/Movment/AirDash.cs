using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AirDash : MonoBehaviour
{

    public Rigidbody rb;

    public InputInfoCenter IIC;
    // Start is called before the first frame update
    public int maxDashes = 3;
    [SerializeField]
    private int dashesLeft= 3;

    public static UnityAction<int> EDashesChanged;

    
    //private 
    private Coroutine airDashIEnumerator;

    private void Awake() {
        EDashesChanged?.Invoke(dashesLeft);
    }

    void Start()
    {
        IsGrounded.ActionOnLanded += OnLanded;
       
    }

    private void OnDestroy()
    {
        IsGrounded.ActionOnLanded -= OnLanded;
        EDashesChanged = null;
    }

    private void OnDisable() {
        if (airDashIEnumerator != null) 
            StopCoroutine(airDashIEnumerator);
        
    }

    void DoDash() {
        Debug.Log("Starting air dash!");
        dashesLeft--;
        airDashIEnumerator = StartCoroutine(IAirDashing(0.4f));
        EDashesChanged?.Invoke(dashesLeft);
    }
    
    void OnLanded()
    {
        if (dashesLeft < maxDashes) 
            dashesLeft = maxDashes;
        
        EDashesChanged?.Invoke(dashesLeft);
        
        if (airDashIEnumerator != null) 
            StopCoroutine(airDashIEnumerator);
    }

    public enum EDashSetMode {
        Addition, SetLimitMax, SetFree
    }
    public void SetDasheseLeft(int num, EDashSetMode _dashSetMode)
    {
        switch (_dashSetMode) {
            case EDashSetMode.Addition:
                dashesLeft += num;
                break;
            case EDashSetMode.SetLimitMax:
                dashesLeft = num;
                dashesLeft = dashesLeft > maxDashes ? maxDashes : dashesLeft;
                break;
            case EDashSetMode.SetFree:
                dashesLeft = num;
                break;
        }


        EDashesChanged?.Invoke(dashesLeft);
    }

    // Update is called once per frame
    void Update()
    {
        if (IIC.controls.Player.Jump.triggered && dashesLeft > 0) {
            DoDash();

        }

        
    }


    
    IEnumerator IAirDashing(float fDuration) {
        Transform mainCamera = Camera.main.transform;
        float AriDashingTime = 0f;
        while (AriDashingTime < fDuration) {
            //gets direction
            Vector3 targetDir = mainCamera.forward;
            targetDir.y = 0f;
            targetDir.Normalize();
            
            Vector3 vel = rb.velocity;
            float velYStored = rb.velocity.y;
            vel.y = 0;

            Vector3 newVel = targetDir * vel.magnitude;
            newVel.y = velYStored;

            rb.velocity = newVel;
            
            
            yield return new WaitForFixedUpdate();
            AriDashingTime += Time.fixedDeltaTime;
            // Debug.Log("Air Dashing!!");
        }
    }
}
