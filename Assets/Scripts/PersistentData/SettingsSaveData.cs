using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsSaveData : MonoBehaviour
{
    public float mouseSensetivity;

    public bool crouchToggle;

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }

}

public interface ISavable
{
    void PopulateSettingsSaveData(SettingsSaveData a_SettingsSaveData);
    void LoadFromSettingsSaveData(SettingsSaveData a_SettingsSaveData);
}
