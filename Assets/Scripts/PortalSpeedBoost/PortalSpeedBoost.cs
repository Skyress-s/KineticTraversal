using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpeedBoost : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField][Tooltip("how much speed to add")]
    private float speedincrease;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rb = other.gameObject.GetComponent<Rigidbody>();
            var v = rb.velocity;
            v += rb.velocity.normalized * speedincrease;
            rb.velocity = v;
        }
    }
}
