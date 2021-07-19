using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelLoad : MonoBehaviour
{
    public SceneReference levelToLoad;


    public void Load()
    {
        //SetCurrentLevelString();

        SceneManager.LoadScene(levelToLoad.ScenePath);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);

    }


    public void Load2()
    {
        var s = GetComponentInChildren<TMP_Text>().text;
        var i = Int32.Parse(s);
        i--;

        //Time.timeScale = 1f;

        LevelManager.loadLevel(i);

    }

}
