using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public delegate void LevelEndEvent();
    public static event LevelEndEvent OnLevelEnd;

    public const float nextLevelSlowdown = 0.2f;

    private void Start()
    {
    }

    private float t;
    private static bool b;

    private void Update()
    {
        if (b)
        {
            t += Time.unscaledDeltaTime;
        }

        if (t > 1f)
        {


            b = false;

            //the actual code for loading a level
            LevelManagerSO LeveData = GetLevelData();

            SceneManager.LoadScene(LeveData.arr[levelToload]);
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            

            //resetes the time scale
            Time.timeScale = 1f;
        }
    }


    public static int levelToload;
    public static int GetLevelIndexCurrentScene()
    {

        

        LevelManagerSO LevelData = GetLevelData();

        Scene currentScene = SceneManager.GetSceneAt(0);
        int i = currentScene.buildIndex;

        i = LevelData.arr.IndexOf(i);

        return i;
    }

    public static void LoadNextLevel()
    {
        LevelManagerSO LevelData = GetLevelData();
        int i = LevelManager.GetLevelIndexCurrentScene();

        loadLevel(i + 1);
        //SceneManager.LoadScene(LevelData.arr[i+1]);
        //SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    public static int GetLevelListLength()
    {
        LevelManagerSO LevelData = GetLevelData();
        return LevelData.arr.Count;
    }

    public static LevelManagerSO GetLevelData()
    {
        return Resources.Load<LevelManagerSO>("LevelData1");
    }
    
    public static void loadLevel(int i)
    {
        Time.timeScale = nextLevelSlowdown;

        b = true;
        levelToload = i;
        
        OnLevelEnd.Invoke();
        //LevelManagerSO LeveData = GetLevelData();



        //SceneManager.LoadScene(LeveData.arr[i]);
        //SceneManager.LoadScene(1, LoadSceneMode.Additive);

        
    }
}

