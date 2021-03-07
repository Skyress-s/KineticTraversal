using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugResetPlayer : MonoBehaviour
{
    public GameObject Player;
    private Rigidbody rb;

    public InputInfoCenter IIC;

    private Keyboard kb;

    // Start is called before the first frame update
    void Start()
    {
        rb = Player.GetComponent<Rigidbody>();
        kb = InputSystem.GetDevice<Keyboard>();
    }

    // Update is called once per frame
    void Update()
    {
        if (kb.tKey.wasPressedThisFrame)
        {
            ResetPlayer();
        }
    }

    public void ResetPlayer()
    {
        Player.transform.position = Vector3.zero;
        rb.velocity = Vector3.zero;

        IIC.grapplingHookStates.currentState = GrapplingHookStates.GHStates.rest;
        IIC.grapplingHookStates.AnimHooked(false);
    }
}
