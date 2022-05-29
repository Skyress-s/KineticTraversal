using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PortalMain : MonoBehaviour
{

    public GameObject[] PortalGO;

    public enum State {active, dorment, inactive}

    public State currentState;
    
    private float stateTime;

    public float cooldown;

    //events
    // public delegate void EnterPortalDelegate(PortalMain portalMain);
    //
    // public static EnterPortalDelegate EEnterPortalBingus;
    
    public static UnityEvent<PortalMain> EEnterPortal = new UnityEvent<PortalMain>();
    
    private void Start()
    {
        currentState = State.inactive;
    }
    private void Update() {
        stateTime += Time.deltaTime;
        switch (currentState)
        {
            case State.active:
                ActiveState();
                break;
            case State.dorment:
                DormantState();
                break;
            case State.inactive:
                InactiveState();
                break;
        }
    }

    private void ActiveState()
    {
        //do someting
    }
    
    private void DormantState()
    {

        if (stateTime > cooldown) //time to wait before jumping to inactive state
        {
            currentState = State.inactive;
        }
    }
    
    private void InactiveState()
    {
        //Do nothing, is inactive
    }

    

    /// <summary>
/// 
/// </summary>
/// <param name="b"> b = true -> enable</param>
    private void DisableEnablePortalColliders(bool b)
    {
        for (int i = 0; i < PortalGO.Length; i++)
        {
            PortalGO[i].GetComponent<MeshCollider>().enabled = b;
        }

    }
    
    //structuers
    struct FPortalConfig {
        
    }

    struct FPortalState {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (currentState == State.inactive) {
            if (other.gameObject.CompareTag("Player")) {
                EEnterPortal?.Invoke(this);
                
                // EEnterPortalBingus?.Invoke(this);
            }
        }
    }
}
