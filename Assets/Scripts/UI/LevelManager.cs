using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public delegate void LevelEndEvent();
    public static event LevelEndEvent OnLevelEnd;

    public const float nextLevelSlowdown = 0.2f;

    [SerializeField] private TextAsset _lastLevelPath;
    
    static string SavePath = "Assets/Resources/LastActiveLevel.txt";
    
    private float t;
    private static bool b;
    private static bool isCustom;

    private void Start() {
        //saves the current path
        
        StreamWriter writer = new StreamWriter(SavePath, false);
        writer.Write(SceneManager.GetActiveScene().path);
        writer.Close();
    }

    private void Update()
    {
        if (b)
        {
            t += Time.unscaledDeltaTime;
        }

        if (t > 1f)
        {

            if (!isCustom)
            {
                b = false;

                //the actual code for loading a level
                LevelManagerSO LeveData = GetLevelData();

                SceneManager.LoadScene(LeveData.arr[levelToload]);
                SceneManager.LoadScene(1, LoadSceneMode.Additive);
            

                //resetes the time scale
                Time.timeScale = 1f;
            }
            else
            {
                b = false;

                LevelManagerSO CustomLevelData = GetCustomLevelsData();

                SceneManager.LoadScene(CustomLevelData.arr[levelToload]);
                SceneManager.LoadScene(1, LoadSceneMode.Additive);
                Time.timeScale= 1f;
            }
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


        //if there are no more levels in the level data, defualt to sanbox level
        if (i + 2 > LevelData.arr.Count)
        {
            loadLevel(0, true);
        }
        else
        {
            loadLevel(i + 1, false);
        }



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

    public static LevelManagerSO GetCustomLevelsData()
    {
        return Resources.Load<LevelManagerSO>("CustomLevels");
    }
    
    public static void loadLevel(int i, bool aIsCustom) {

     
        
        Time.timeScale = nextLevelSlowdown;
        isCustom = aIsCustom;
        b = true;
        levelToload = i;
        
        OnLevelEnd.Invoke();


    }

    [ContextMenu("lastActiveLevelDuringPlay")]
    public void LoadLastLevel() {
        StreamReader reader = new StreamReader(SavePath);
        string path = reader.ReadToEnd(); 
        reader.Close();
        EditorSceneManager.OpenScene(path);
        EditorSceneManager.OpenScene("Assets/Scenes/PlayerScene2.unity", OpenSceneMode.Additive);
        
    }
}

