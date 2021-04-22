using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoad : MonoBehaviour
{
    public SceneReference levelToLoad;


    public void Load()
    {
        SetCurrentLevelString();

        Time.timeScale = 1;
        SceneManager.LoadScene(levelToLoad.ScenePath);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);

    }

    public void SetCurrentLevelString()
    {
        var s = gameObject.GetComponentInChildren<TMPro.TMP_Text>().text;
        CurrentLevelIndicator.currentLevel = s;
    }
}
