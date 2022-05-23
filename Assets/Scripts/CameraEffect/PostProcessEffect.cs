using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class PostProcessEffect : ScriptableObject
{
    public float Duration = 1f;
    public AnimationCurve Curve;
    public VolumeProfile PostProcessProfile;
    
}
