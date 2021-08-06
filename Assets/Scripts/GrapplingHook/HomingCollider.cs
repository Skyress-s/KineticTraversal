using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingCollider : MonoBehaviour
{
    public GrapplingHookStates GHS;

    public Rigidbody rb;

    private bool homing;

    public Transform target;

    private void Start()
    {
        GrapplingHookStates.firingEvent += EnableTrigger;
        GrapplingHookStates.hooked += DisableTrigger;
        GrapplingHookStates.hookRetractedEvent += DisableTrigger;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GHS.currentState == GrapplingHookStates.GHStates.travling && ExperimentalTags.IsHookable(other.gameObject))
        {
            //Debug.Log("detected!");
            homing = true;
            target = other.transform;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        homing = false;
        target = null;
    }

    private void FixedUpdate()
    {
        if (homing)
        {
            Debug.Log("HOMING!");
            DragTowardsTarget();
        }

    }

    private void OnDrawGizmos()
    {
        if (target != null)
        {
            Gizmos.DrawSphere(target.GetComponent<MeshCollider>().ClosestPoint(rb.transform.position), 1f);
        }
    }

    private void EnableTrigger()
    {
        GetComponent<MeshCollider>().enabled = true;
    }

    private void DisableTrigger()
    {
        GetComponent<MeshCollider>().enabled = false;
    }

    private void DragTowardsTarget()
    {
        float rotateAmoundt = 0.03f;


        var vel = rb.velocity;

        Vector3 targetpos = target.GetComponent<MeshCollider>().ClosestPoint(rb.transform.position);

        

        Vector3 dir = targetpos - rb.transform.position;
        dir = dir.normalized;

        var newVel = Vector3.RotateTowards(vel, dir, rotateAmoundt, 0f);

        //rotates the obj by the same amoundt
        Quaternion targetRot = Quaternion.LookRotation(dir, Vector3.up);

        rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, targetRot, Mathf.Rad2Deg * rotateAmoundt);
        
        rb.velocity = newVel;

        //rb.velocity = vel * 10f;
    }
}
