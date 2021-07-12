using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// simple script that has a public bool if its triggerd or not (used for wallrunning)
/// </summary>
public class ColliderTrigger : MonoBehaviour
{

    private int i;

    [SerializeField]
    private bool DebugisTriggerd;

    /// <summary>
    ///overloadable bool; adds one and minus whem when setting to true and false; if i > 0, it return true
    /// </summary>
    public bool isTriggerd
    {
        get
        {
            if (i > 0)
            {
                return true;
            }
            else return false;
            SetViableBool(isTriggerd);
        }
        set
        {
            if (value == true)
            {
                i++;
            }
            else
            {
                i--;
            }
            SetViableBool(isTriggerd);
        }
    }

    private void SetViableBool(bool b)
    {
        DebugisTriggerd = b;
    }

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
