using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance { get; private set; }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this);
        }
        else {
            instance = this;
            Control = new PlayerControl();
        }
    }
    
    [HideInInspector]
    public PlayerControl Control;
    
    
    private void OnEnable()
    {
        Control.Enable();
    }
    private void OnDisable()
    {
        Control.Disable();
    }
}
