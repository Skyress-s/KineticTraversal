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

    private bool isOnWall;

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
            else
            {
                return false;
            }
        }
        set
        {
            if (value == true)
            {
                i++;
                SetViableBool(true);
            }
            else
            {
                i--;
                SetViableBool(false);
            }
        }
    }

    private void SetViableBool(bool b)
    {
        DebugisTriggerd = b;
    }

    private void FixedUpdate()
    {
        if (!isOnWall)
        {
            isTriggerd = false;
        }


        //since ontrigger updates happen after fixed update this runs last to check if we are acually on a wall
        isOnWall = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        isTriggerd = true;
    }

    private void OnTriggerStay(Collider other)
    {
        isOnWall = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggerd = false;
    }
}
