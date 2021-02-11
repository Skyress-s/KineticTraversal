using System.Collections;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewInfoCenterData", menuName = "InfoCenterData", order = 1)]
public class InfoCenterData : ScriptableObject
{
    public bool Isgrounded;

    public float speed;


    void Update()
    {
    }
}
