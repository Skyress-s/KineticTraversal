using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingOBSTrigger : MonoBehaviour
{
    [SerializeField]
    private float maxSpeedIncrease, speedIncrease;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var rb = other.GetComponent<Rigidbody>();
            if (rb.velocity.sqrMagnitude < maxSpeedIncrease * maxSpeedIncrease)
            {
                rb.velocity = rb.velocity + rb.velocity.normalized * speedIncrease;
            }
        }
    }


}
