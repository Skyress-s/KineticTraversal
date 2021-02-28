using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugResetPlayer : MonoBehaviour
{
    public GameObject Player;
    private Rigidbody rb;

    public InputInfoCenter IIC;

    // Start is called before the first frame update
    void Start()
    {
        rb = Player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Player.transform.position = Vector3.zero;
            rb.velocity = Vector3.zero;

            IIC.grapplingHookStates.currentState = GrapplingHookStates.GHStates.rest;
        }
    }
}
