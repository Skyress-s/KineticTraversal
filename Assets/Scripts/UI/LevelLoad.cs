using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{
    public SceneReference levelToLoad;


    public void Load()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelToLoad.ScenePath);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
}
