using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AirDashesUI : MonoBehaviour
{
    public AirDash _AirDash;

    public GameObject DashIcon;

    List<GameObject> Dashes;

    private void Start()
    {
        AirDash.testAction += OnValueChanged;
        Dashes = new List<GameObject>();
    }

    private void OnDestroy()
    {
        AirDash.testAction -= OnValueChanged;
    }

    void OnValueChanged(int num)
    {
        for (int i = 0; i < Dashes.Count; i++)
        {
            Destroy(Dashes[i]);
            
        }
        Dashes.Clear();

        

        for (int i = 0; i < num; i++)
        {
            GameObject temp = Instantiate(DashIcon, gameObject.transform);
            Dashes.Add(temp);
        }
        
        
    }

}
