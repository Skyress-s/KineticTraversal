using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class TempDebugTesting : MonoBehaviour
{
   
    public LevelManagerSO LevelData;

    //portion for new Level indicator and managing system
    //idea:
    //use a unity Scenemanager build order as a the int value in the list


    private Keyboard kb;
    // Start is called before the first frame update
    void Start()
    {
        kb = InputSystem.GetDevice<Keyboard>();
    }

    // Update is called once per frame
    void Update()
    {
        //loads the next level i queue
        if (kb.nKey.wasPressedThisFrame)
        {


            LevelManager.LoadNextLevel();
            ////gets the active scene level
            //Scene activeScene = SceneManager.GetSceneAt(0);
            
            //int i = activeScene.buildIndex;
            

            ////gets the index number [x] of the level order Data Array
            //var n = LevelData.arr.IndexOf(i);


            ////then loads the next scene in order of the levelData

            //SceneManager.LoadScene(LevelData.arr[n + 1]);
            //SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
    }
}
