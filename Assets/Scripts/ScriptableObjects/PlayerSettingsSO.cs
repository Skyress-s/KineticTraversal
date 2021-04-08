using System.Collections;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewPlayerSettings", menuName = "PlayerSettings/NewplayerSettings", order = 1)]
public class PlayerSettingsSO : ScriptableObject
{
    public float sensetivity;
    public Sliding.SlidingMode SlidingMode;
    public char CrouchKey;
        
}