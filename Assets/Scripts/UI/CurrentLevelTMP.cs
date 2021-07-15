using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class CurrentLevelTMP : MonoBehaviour
{
    public TMPro.TMP_Text currentLevelString;

    private GameObject go;

    public LevelManagerSO LevelData;

    void Start()
    {

        //new method
        int i = LevelManager.GetLevelIndexCurrentScene();
        i++;


        currentLevelString.text = i.ToString();
    }
}
