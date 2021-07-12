using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugGetSetTemp : MonoBehaviour
{
    public int i;
    public bool triggerd
    {
        get
        {
            if (i > 0)
            {
                return true;
            }
            else return false;
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
        }
    }


    public List<int> arr;

    private Keyboard kb;
    // Start is called before the first frame update
    void Start()
    {
        kb = InputSystem.GetDevice<Keyboard>();

        arr.Add(1);
        arr.Add(2);
        arr.Add(3);
        arr.Add(4);

        arr.Insert(2, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (kb.mKey.wasPressedThisFrame)
        {
            triggerd = true;
        }

        if (kb.nKey.wasPressedThisFrame)
        {
            triggerd = false;
        }


    }
}
