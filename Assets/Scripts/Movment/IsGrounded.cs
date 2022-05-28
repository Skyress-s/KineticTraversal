using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class IsGrounded : MonoBehaviour
{

    public bool _isgrounded;

    public float maxRayDistance;

    private int layermask;

    public static UnityAction ActionOnLanded;

    public static UnityAction ActionOnJump;

    private void Awake()
    {
        layermask = 1 << 9; // makes a bitmask -> only hits playerlayer
        layermask = ~layermask; //inverser bitmask -> hits everything exept playerlayer
    }

    private void Update()
    {
        // Debug.DrawRay(transform.position, -transform.up, Color.blue,  maxRayDistance);
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, -transform.up, out hit, maxRayDistance, layermask, QueryTriggerInteraction.Ignore))
        {
            if (KineticTags.IsWalkable(hit.transform.gameObject) && !_isgrounded) // on landed
            {
                _isgrounded = true;
                if (ActionOnLanded != null)
                    ActionOnLanded.Invoke();
            }


        }
        else if (_isgrounded)
        {
            if (ActionOnJump != null)
                ActionOnJump.Invoke();
            _isgrounded = false;
        }
    }
}
