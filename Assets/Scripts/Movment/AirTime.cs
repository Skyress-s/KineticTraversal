using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTime : MonoBehaviour
{
    public InputInfoCenter IIC;

    public float airTime;
    // Update is called once per frame
    void Update()
    {
        if (IIC.grounded._isgrounded)
        {
            airTime = 0f;
        }
        else
        {
            airTime += Time.deltaTime;
        }
        //Debug.Log(airTime);
    }
}


