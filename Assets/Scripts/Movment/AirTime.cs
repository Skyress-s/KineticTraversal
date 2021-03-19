using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTime : MonoBehaviour
{
    public InputInfoCenter IIC;

    public float airTime;

    public bool b_airTime;
    // Update is called once per frame
    void Update()
    {
        if (IIC.grounded._isgrounded)
        {
            airTime = 0f;
            b_airTime = false;
        }
        else
        {
            airTime += Time.deltaTime;
            b_airTime = true;
        }
        //Debug.Log(airTime);
    }
}


