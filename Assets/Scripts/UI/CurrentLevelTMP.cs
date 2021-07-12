using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CurrentLevelTMP : MonoBehaviour
{
    public TMPro.TMP_Text currentLevelString;

    private GameObject go;

    void Start()
    {
        try
        {
            // tries to get the 
            go = GameObject.FindGameObjectWithTag("UniqueLevelData");
            currentLevelString.text = go.GetComponent<UniqueLevelData>().level.ToString();

        }
        catch (Exception)
        {
            Debug.LogWarning("no Current Level indicator found for this scene");
        }
        
        
    }
}
