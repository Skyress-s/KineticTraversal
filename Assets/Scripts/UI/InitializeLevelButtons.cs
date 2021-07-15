using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InitializeLevelButtons : MonoBehaviour
{
    private bool _hasActivated = false;

    public GameObject ButtonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _hasActivated = true;


        for (int i = 0; i < LevelManager.GetLevelListLength(); i++)
        {
            var button =  GameObject.Instantiate(ButtonPrefab, gameObject.transform);

            int n = i + 1;
            var text = button.GetComponentInChildren<TMP_Text>().text = n.ToString();
        }

    }
}
