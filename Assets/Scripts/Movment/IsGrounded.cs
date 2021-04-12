using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IsGrounded : MonoBehaviour
{

    public bool _isgrounded;

    public float maxRayDistance;

    private int layermask;

    private void Awake()
    {
        layermask = 1 << 9; // makes a bitmask -> only hits playerlayer
        layermask = ~layermask; //inverser bitmask -> hits everything exept playerlayer
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, -transform.up, Color.blue,  maxRayDistance);
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, -transform.up, out hit, maxRayDistance, layermask, QueryTriggerInteraction.Ignore))
        {
            _isgrounded = true;
            //Debug.Log(hit.collider.gameObject.name);
        }
        else
        {
            _isgrounded = false;
        }
    }
}
