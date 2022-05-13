using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private PostProcessProfileEffect effect1;


    public void PlayPostProcessEffect(PostProcessProfileEffect PPPE) {
        Volume volumeComp = gameObject.AddComponent<Volume>();
        volumeComp.isGlobal = true;
        volumeComp.profile = PPPE.profile;

        // StartCoroutine(IPlayPostProcessEffect);
    }

    
    
    /*IEnumerator IPlayPostProcessEffect(Volume volume) {
        
    
    
        /*float time = 0f;
        while (time < ) {
            yield return new WaitForEndOfFrame();
            
        
        
        

        }#1#
    
        
    
    }*/
}



[Serializable]
public class PostProcessProfileEffect
{
    public float duration = 1f;
    public AnimationCurve curve;
    public VolumeProfile profile;
}
