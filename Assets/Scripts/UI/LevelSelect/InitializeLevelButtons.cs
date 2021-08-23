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
        

        //then adds the normal level
        for (int i = 0; i < LevelManager.GetLevelListLength(); i++)
        {
            var button =  GameObject.Instantiate(ButtonPrefab, gameObject.transform);

            int n = i + 1;
            button.GetComponentInChildren<TMP_Text>().text = n.ToString();


            //new method to load scene
            var LBD = button.GetComponent<LevelButtonData>();
            LBD.levelToload = i;
            LBD.isCustomLevel = false;
        }

        //first adds the anormal levels
        var buutton = Instantiate(ButtonPrefab, gameObject.transform);
        buutton.GetComponentInChildren<TMP_Text>().text = "Sandbox";
        buutton.GetComponentInChildren<TMP_Text>().fontSize = 40f;

        var SandboxLBD = buutton.GetComponent<LevelButtonData>();
        SandboxLBD.levelToload = 0;
        SandboxLBD.isCustomLevel = true;
    }
}
