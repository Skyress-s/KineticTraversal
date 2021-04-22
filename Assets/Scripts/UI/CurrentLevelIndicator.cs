using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CurrentLevelIndicator : MonoBehaviour
{
    public static string currentLevel;

    public static bool hasLoaded;
    // Start is called before the first frame update
    void Start()
    {
        if (CurrentLevelIndicator.hasLoaded)
        {
            DestroyImmediate(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        CurrentLevelIndicator.hasLoaded = true;
    }
}
