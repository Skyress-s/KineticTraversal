using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.InputSystem;

public class HookVFX : MonoBehaviour
{
    public GameObject KnockoffPrefab;

    public VisualEffect[] VFX_Array;

    [SerializeField]
    private int n;

    private void Update()
    {
        var kb = InputSystem.GetDevice<Keyboard>();
        if (kb.upArrowKey.wasPressedThisFrame)
        {
            n++;
        }
        else if (kb.downArrowKey.wasPressedThisFrame)
        {
            n--;
        }

        if (kb.rightArrowKey.wasPressedThisFrame)
        {
            PlayAnimation(n, transform.position, transform.rotation);
        }

        
    
    }


    public void PlayKnockoffVFX(Vector3 pos, Quaternion rot, Transform parent)
    {
        var con = Instantiate(KnockoffPrefab, pos, rot);
        con.transform.parent = parent;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="i">0 - Hooked, 1 - Fire</param>
    public void PlayAnimation(int i, Vector3 pos, Quaternion rot)
    {
        if (i == 2)
        {
            //Instantiate(KnockoffPrefab, pos, rot);
        }
        else
        {
            VFX_Array[i].Play();
        }       
    }

    

}
