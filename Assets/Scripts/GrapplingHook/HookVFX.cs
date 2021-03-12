using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.InputSystem;

public class HookVFX : MonoBehaviour
{
    public VisualEffect VFXObject;

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
            PlayAnimation(n);
        }

        
    
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="i">0 - Hooked, 1 - Fire</param>
    public void PlayAnimation(int i)
    {
        //VFXObject.visualEffectAsset = VFX_Array[i];
        //VFXObject.Play();
        VFX_Array[i].Play();
    }

    

}
