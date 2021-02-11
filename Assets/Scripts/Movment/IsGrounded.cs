using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IsGrounded : MonoBehaviour
{

    public bool _isgrounded;

    public float maxRayDistance;

    private void Update()
    {
        Debug.DrawRay(transform.position, -transform.up, Color.blue,  maxRayDistance);
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, -transform.up, out hit, maxRayDistance))
        {
            _isgrounded = true;
        }
        else
        {
            _isgrounded = false;
        }
    }
}
