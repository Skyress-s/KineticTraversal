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
    void Start()
    {
        IsGrounded.ActionOnLanded += OnLanded;
        EDashesChanged?.Invoke(dashesLeft);
    }

    private void OnDestroy()
    {
        IsGrounded.ActionOnLanded -= OnLanded;
    }

    private void OnDisable() {
        if (airDashIEnumerator != null) 
            StopCoroutine(airDashIEnumerator);
        
    }

    void DoDash() {
        
        /*Vector3 dir = Camera.main.transform.forward;
        float mag = rb.velocity.magnitude;

        dir = dir * 2f + rb.velocity.normalized;
        
        dir.Normalize();


        rb.velocity = dir * mag;
        dashesLeft--;
        */
        Debug.Log("Starting air dash!");
        airDashIEnumerator = StartCoroutine(IAirDashing(2f));
        EDashesChanged?.Invoke(dashesLeft);
    }
    
    void OnLanded()
    {
        dashesLeft = maxDashes;
        EDashesChanged?.Invoke(dashesLeft);
        
        StopCoroutine(airDashIEnumerator);
    }

    public void SetDasheseLeft(int num, bool bAddition)
    {
        if (bAddition)
            dashesLeft += num;
        else
            dashesLeft = num;

        
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
            Vector3 dir = mainCamera.forward;
            dir.y = 0f;
            dir.Normalize();
            
            Vector3 vel = rb.velocity;
            float velYStored = rb.velocity.y;
            vel.y = 0;

            Vector3 newVel = dir * vel.magnitude;
            newVel.y = velYStored;

            rb.velocity = newVel;
            
            
            yield return new WaitForFixedUpdate();
            AriDashingTime += Time.fixedDeltaTime;
            Debug.Log("Air Dashing!!");
        }
    }
}
