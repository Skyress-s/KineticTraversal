using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

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

        SceneManager.LoadScene(LevelData.arr[i+1]);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
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
        LevelManagerSO LeveData = GetLevelData();

        SceneManager.LoadScene(LeveData.arr[i]);
        SceneManager.LoadScene(1,LoadSceneMode.Additive);
    }
    

}
