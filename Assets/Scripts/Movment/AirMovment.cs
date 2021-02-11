using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirMovment : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            var v = rb.velocity;
            v.y = 0f;
            //v = v.normalized;


            rb.AddForce(-v.normalized * v.magnitude * 10f * Time.deltaTime, ForceMode.VelocityChange);
        }
    }
}
