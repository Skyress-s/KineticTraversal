using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        LevelManager.LoadNextLevel();
    }
}
