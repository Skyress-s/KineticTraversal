using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateVariables : MonoBehaviour, ISavable
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
        LoadFromJson();
    }
    
    public void FromUIToJsonAndGame()
    {
        UIToJson();
        LoadFromJson();
    }


    public void UIToJson()
    {
        SettingsSaveData ssd = new SettingsSaveData();

        PopulateSettingsSaveData(ssd);

        if (FileManager.WriteToFile("SettingsSaveData.dat", ssd.ToJson()))
        {
            Debug.Log("SaveCompleted!");
        }
    }
    
    /// <summary>
    /// updates BOTH UI and InGame
    /// </summary>
    /// <param name="a_SettingsSaveData"></param>
    public void LoadFromJson()
    {

        if (FileManager.LoadFromFile("SettingsSaveData.dat", out var json))
        {
            SettingsSaveData ssd = new SettingsSaveData();
            ssd.LoadFromJson(json);

            LoadFromSettingsSaveData(ssd);
            Debug.Log("Load Complete!");
        }

    }

   

    //old Method
    
      public void FromIngameSOToUI()
    {
        sensetivitySlider.value = IngameSettings.sensetivity;
        CrouchKeyInput.text = IngameSettings.CrouchKey.ToString();
        if (IngameSettings.SlidingMode == Sliding.SlidingMode.hold) ToggleCrouch.isOn = false;
        else ToggleCrouch.isOn = true;
        
    }

    public void FromUIToIngameSO()
    {
        IngameSettings.sensetivity = sensetivitySlider.value;
        char[] var = CrouchKeyInput.text.ToCharArray();
        IngameSettings.CrouchKey = var[0];
        if (ToggleCrouch.isOn) IngameSettings.SlidingMode = Sliding.SlidingMode.toggle;
        else IngameSettings.SlidingMode = Sliding.SlidingMode.hold;

    }

    public void FromIngameSOToInGame()
    {
        cameraLook.sensetivity = IngameSettings.sensetivity;
        SlidingScript.SlidingKey = (KeyCode)IngameSettings.CrouchKey;
        SlidingScript.currentSlidingMode = IngameSettings.SlidingMode;
    }

    public void PopulateSettingsSaveData(SettingsSaveData a_SettingsSaveData)
    {
        a_SettingsSaveData.mouseSensetivity = sensetivitySlider.value;

        a_SettingsSaveData.crouchToggle = ToggleCrouch.isOn;
    }


    /// <summary>
    /// updates BOTH in game settings AND UI
    /// </summary>
    /// <param name="a_SettingsSaveData"></param>
    public void LoadFromSettingsSaveData(SettingsSaveData a_SettingsSaveData)
    {
        //mouse sensetivity
        sensetivitySlider.value = a_SettingsSaveData.mouseSensetivity;
        cameraLook.sensetivity = a_SettingsSaveData.mouseSensetivity;

        //crouch toggle
        ToggleCrouch.isOn = a_SettingsSaveData.crouchToggle;
        if (a_SettingsSaveData.crouchToggle) SlidingScript.currentSlidingMode = Sliding.SlidingMode.toggle;
        else SlidingScript.currentSlidingMode = Sliding.SlidingMode.hold;
        
    }
}
