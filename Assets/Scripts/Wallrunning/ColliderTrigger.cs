using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// simple script that has a public bool if its triggerd or not (used for wallrunning)
/// </summary>
public class ColliderTrigger : MonoBehaviour
{
    public bool isTriggerd;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        isTriggerd = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isTriggerd = false;
    }
}
