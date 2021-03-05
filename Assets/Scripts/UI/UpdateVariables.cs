using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateVariables : MonoBehaviour
{
    [Header("Mouse sensetivity")]
    [Space]
    public CameraLook cameraLook;

    public Slider sensetivitySlider;

    [Header("Toggle Crouch")]
    [Space]
    public Toggle ToggleCrouch;

    public Sliding SlidingScript;

    public TMPro.TMP_InputField CrouchKeyInput;

    public PlayerSettingsSO OnAwakeSettings;

    [Tooltip("This is for using ingame/between levels (prone to change during game)")]
    public PlayerSettingsSO IngameSettings;


    private void Start()
    {
        //On awake load, the current defualt settings file
        //DoUpdateVariables();
        LoadVariablesToIngameSettingsSO();
        FromIngameSOToUI();
        FromIngameSOToGame();
    }
    
    /// <summary>
    /// Loads the variables fomr the settings on Awake file and loads it into allt relevent places
    /// </summary>
    public void LoadVariablesToIngameSettingsSO()
    {
        if (OnAwakeSettings.HasLoadedDeafualt == true)
        {
            return;
        }
        OnAwakeSettings.HasLoadedDeafualt = true;


        //games the ingamesettings load the awake settings
        IngameSettings.sensetivity = OnAwakeSettings.sensetivity;
        IngameSettings.SlidingMode = OnAwakeSettings.SlidingMode;
        IngameSettings.CrouchKey = OnAwakeSettings.CrouchKey;

        ////applies them to ingame relevant places
        //cameraLook.sensetivity = OnAwakeSettings.sensetivity;
        //SlidingScript.currentSlidingMode = OnAwakeSettings.SlidingMode;
        //SlidingScript.SlidingKey = (KeyCode)OnAwakeSettings.CrouchKey;

        ////applies them to settings UI
        //sensetivitySlider.value = OnAwakeSettings.sensetivity;
        //if (OnAwakeSettings.SlidingMode == Sliding.SlidingMode.toggle) ToggleCrouch.isOn = true;
        //else ToggleCrouch.isOn = false;
    }
    public void FromIngameSOToUI()
    {
        sensetivitySlider.value = IngameSettings.sensetivity;
        CrouchKeyInput.text = IngameSettings.CrouchKey.ToString();
        if (IngameSettings.SlidingMode == Sliding.SlidingMode.hold) ToggleCrouch.isOn = false;
        else ToggleCrouch.isOn = true;
        
    }

    public void FromSettingsUIToIngameSO()
    {
        IngameSettings.sensetivity = sensetivitySlider.value;
        char[] var = CrouchKeyInput.text.ToCharArray();
        IngameSettings.CrouchKey = var[0];
        if (ToggleCrouch.isOn) IngameSettings.SlidingMode = Sliding.SlidingMode.toggle;
        else IngameSettings.SlidingMode = Sliding.SlidingMode.hold;

    }

    public void FromIngameSOToGame()
    {
        cameraLook.sensetivity = IngameSettings.sensetivity;
        SlidingScript.SlidingKey = (KeyCode)IngameSettings.CrouchKey;
        SlidingScript.currentSlidingMode = IngameSettings.SlidingMode;
    }

    public void DoUpdateVariables()
    {
        cameraLook.sensetivity = IngameSettings.sensetivity;
        SlidingScript.currentSlidingMode = IngameSettings.SlidingMode;
        SlidingScript.SlidingKey = (KeyCode)IngameSettings.CrouchKey;

    }
}
