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

    public static UnityAction<int> OnDashesLeftChanged;


    void Start()
    {
        IsGrounded.ActionOnLanded += OnLanded;
       
    }

    private void OnDestroy()
    {
        IsGrounded.ActionOnLanded -= OnLanded;
    }


    void DoDash() {
        Vector3 dir = Camera.main.transform.forward;
        float mag = rb.velocity.magnitude;

        rb.velocity = dir * mag;
        dashesLeft--;
    }


    void OnLanded()
    {
        dashesLeft = maxDashes;
        if (OnDashesLeftChanged != null)
            OnDashesLeftChanged.Invoke(dashesLeft);
    }

    public void SetDasheseLeft(int num, bool bAddition)
    {
        if (bAddition)
            dashesLeft += num;
        else
            dashesLeft = num;

        
        if (OnDashesLeftChanged != null)
        {
            OnDashesLeftChanged.Invoke(dashesLeft);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (IIC.controls.Player.Jump.triggered && dashesLeft > 0) {
            DoDash();
            if (OnDashesLeftChanged != null)
                OnDashesLeftChanged.Invoke(dashesLeft);
            
        }

        

        //if (IIC.grounded._isgrounded) {
        //    dashesLeft = maxDashes;
        //}
    }
}
