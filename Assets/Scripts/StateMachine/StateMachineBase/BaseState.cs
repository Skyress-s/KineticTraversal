using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState {

    //this is mostly for enabling disabling debugging
    private StateHandlerBase _stateHandlerBase;

    public float StateTime = 0f;
    
    protected BaseState(StateHandlerBase stateHandlerBase) {
        _stateHandlerBase = stateHandlerBase;
    }
    public virtual void Enter() {
        if (_stateHandlerBase._bDebugState)
            Debug.Log("Entering state " + this.GetType().Name);
    }
    public virtual void Update() {
        StateTime += Time.deltaTime;
        if (_stateHandlerBase._bDebugState)
            Debug.Log(this.GetType().Name);
    }


    public virtual void Exit() {
        if (_stateHandlerBase._bDebugState)
            Debug.Log("Exiting state " + this.GetType().Name);
    }
}
