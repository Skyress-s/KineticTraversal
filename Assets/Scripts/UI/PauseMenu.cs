using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public InputInfoCenter IIC;

    public static bool gameIsPaused;

    public GameObject PauseMenuUI;

    public GameObject SettingsMenuUI;

    public GameObject HUDUI;

    public GameObject LevelSelectUI;

    public UpdateVariables UpdateVariablesScript;

    public SceneReference test;

    public Scene test2;


    private void Start()
    {
        gameIsPaused = false;
    }
    void Update()
    {
        if (IIC.controls.Player.Pause.triggered)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    
    public void Resume()
    {
        LockMouse();
        PauseMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(false);
        LevelSelectUI.SetActive(false);
        HUDUI.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;
        UpdateVariables();
    }

    void Pause()
    {
        FreeMouse();
        PauseMenuUI.SetActive(true);
        HUDUI.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void OpenSettings()
    {
        //disables pause menu
        PauseMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(true);
        LevelSelectUI.SetActive(false);
    }

    public void OpenPauseMenu()
    {
        PauseMenuUI.SetActive(true);
        SettingsMenuUI.SetActive(false);
        LevelSelectUI.SetActive(false);
    }

    public void OpenLevelSelect()
    {
        PauseMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(false);
        LevelSelectUI.SetActive(true);
    }

    public void UpdateVariables()
    {
        //UpdateVariablesScript.DoUpdateVariables();
        UpdateVariablesScript.FromSettingsUIToIngameSO();
        UpdateVariablesScript.FromIngameSOToGame();
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    /// <param name ="float"> the float is a float </param>
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void OpenLevelTest(SceneReference level)
    {
        
    }

    public void OpenLevel(int i)
    {
        Debug.Log("Loading level "+ i);
        Resume();
        SceneManager.LoadScene(i);
        SceneManager.LoadScene(0, LoadSceneMode.Additive);
    }

    public void LockMouse()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void FreeMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
