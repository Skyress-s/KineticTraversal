using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStartScenes : MonoBehaviour
{
    public SceneReference PlayerScene;

    public SceneReference LevelToLoad;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(LevelToLoad, LoadSceneMode.Single);
        SceneManager.LoadScene(PlayerScene, LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
