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

    void Start()
    {
        IsGrounded.ActionOnLanded += OnLanded;
        EDashesChanged?.Invoke(dashesLeft);
    }

    private void OnDestroy()
    {
        IsGrounded.ActionOnLanded -= OnLanded;
    }


    void DoDash() {
        Vector3 dir = Camera.main.transform.forward;
        float mag = rb.velocity.magnitude;

        dir = dir * 2f + rb.velocity.normalized;
        
        dir.Normalize();


        rb.velocity = dir * mag;
        dashesLeft--;
        
        EDashesChanged?.Invoke(dashesLeft);
    }
    
    void OnLanded()
    {
        dashesLeft = maxDashes;
        EDashesChanged?.Invoke(dashesLeft);
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
}
