using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{

    public SceneReference sr;

    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(sr.ScenePath);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
}
