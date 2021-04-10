using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSave : MonoBehaviour, ISavable
{
    public float currentSensetivity;

    public void PopulateSettingsSaveData(SettingsSaveData a_SettingsSaveData)
    {
        a_SettingsSaveData.mouseSensetivity = currentSensetivity;
    }

    public void LoadFromJsonSaveData()
    {
        if (FileManager.LoadFromFile("settingsSaveData.dat", out var json))
        {
            SettingsSaveData ssd = new SettingsSaveData();
            ssd.LoadFromJson(json);

            LoadFromSettingsSaveData(ssd);
            Debug.Log("Load complete!");
        }
    }

    public void LoadFromSettingsSaveData(SettingsSaveData a_SettingSaveData)
    {
        currentSensetivity = a_SettingSaveData.mouseSensetivity;
    }

   
    public void SaveJsonSettingsData()
    {
        SettingsSaveData ssd = new SettingsSaveData();
        PopulateSettingsSaveData(ssd);


        if (FileManager.WriteToFile("settingsSaveData.dat", ssd.ToJson()))
        {
            Debug.Log("Save completed!");
        }
    }


    private void Start()
    {
        //SaveJsonSettingsData();
        //Debug.Log(Application.persistentDataPath);

        //LoadFromJsonSaveData();

    }

}
