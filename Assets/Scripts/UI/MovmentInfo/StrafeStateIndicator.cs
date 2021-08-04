using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StrafeStateIndicator : MonoBehaviour
{
    public InputInfoCenter IIC;

    public Color strafeSharpColor, strafeBluntColor, strafeBrakeColor, notstrafingColor;

    public Image Target;

    private Color col;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IIC._AirMovment.enabled == false)
        {
            Target.color = notstrafingColor;
            return;
        }

        if (IIC._AirMovment.currentStrafeState == AirMovment.StrafeState.sharpStrafe)
        {
            col = strafeSharpColor;
        }
        else if (IIC._AirMovment.currentStrafeState == AirMovment.StrafeState.BluntStrafe)
        {
            col = strafeBluntColor;
        }
        else if (IIC._AirMovment.currentStrafeState == AirMovment.StrafeState.AirBrake)
        {
            col = strafeBrakeColor;
        }
        else
        {
            col = notstrafingColor;
        }

        Target.color = Color.Lerp(Target.color, col, 20f * Time.deltaTime);

    }
}
