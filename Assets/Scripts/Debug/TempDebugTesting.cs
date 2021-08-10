using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class TempDebugTesting : MonoBehaviour
{
   
    public LevelManagerSO LevelData;

    public Rigidbody playerRB;

    public GrapplingHookStates GHS;


    public float pushDownAmoundt;

    //portion for new Level indicator and managing system
    //idea:
    //use a unity Scenemanager build order as a the int value in the list


    private Keyboard kb;
    // Start is called before the first frame update
    void Start()
    {
        kb = InputSystem.GetDevice<Keyboard>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR

        if (kb.fKey.wasPressedThisFrame && GHS.currentState != GrapplingHookStates.GHStates.hooked)
        {
            Debug.Log("Pushind down");
            playerRB.AddForce(Vector3.down * pushDownAmoundt,ForceMode.VelocityChange);
        }

        if (kb.fKey.wasReleasedThisFrame && GHS.currentState != GrapplingHookStates.GHStates.hooked)
        {
            playerRB.AddForce(Vector3.up * pushDownAmoundt,ForceMode.VelocityChange);
        }
#endif
    }
}
