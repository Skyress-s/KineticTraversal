using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirStrafe : MonoBehaviour
{
    private Rigidbody rb;

    private Transform cameraTrans;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTrans = Camera.main.transform;
    }

    float _yrotCamstored = 0f;
    void Update()
    {
        float _ystored = rb.velocity.y;

        
        // Gets the direction where the player is moving
        var dir = rb.velocity;
        dir = RemoveYcomp(dir);

        // Gets the dir where the player is looking
        var camdir = cameraTrans.forward;
        camdir = RemoveYcomp(camdir);

        // Finds the angle between them
        var angle = Vector3.Angle(camdir, dir);


        // Getting left or right
        float _signedAngle = Vector3.SignedAngle(camdir, dir, Vector3.up);
        if (Input.GetKey(KeyCode.D) && _signedAngle < 0)
        { // Right
            //Debug.Log("RIGHT");
            Airstafe();
        }
        else if (Input.GetKey(KeyCode.A) && _signedAngle > 0)
        { // Left
            //Debug.Log("LEFT");
            Airstafe();
        }
        else
        {
            // Debug.Log("Turn: not detected");
        }

        void Airstafe()
        {
            if (angle < 1079 * Time.deltaTime)
            {
                camdir.Normalize();
                var vel = rb.velocity;
                vel = RemoveYcomp(vel);
                float speed = vel.magnitude;

                rb.velocity = new Vector3(camdir.x * speed, rb.velocity.y, camdir.z * speed);
            }
            _yrotCamstored = cameraTrans.rotation.y;
        }
    }

    private Vector3 RemoveYcomp(Vector3 vect)
    {
        vect = new Vector3(vect.x, 0, vect.z);
        return vect;
    }
}
