using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAirState : PlayerBaseState
{
    public float t = 0f;
    public override void EnterState(Context player)
    {
        t = 0f;

        //Debug.Log("Entering AirState...");

        player.IIC._AirMovment.enabled = true;
<<<<<<< HEAD
        player.IIC.WallrunDetect.enabled = true;
=======
        player.IIC.wallrunning.enabled = true;
>>>>>>> 2530c0655a5d19272e2570d7a60403da8165c159

        player.Wallrun2.enabled = false;
        player.IIC.GroundMovment.enabled = false;
        player.IIC.infoSliding.enabled = false;
    }

    public override void Update(Context player)
    {
<<<<<<< HEAD
        //Debug.Log("In AirStatee");
=======
        Debug.Log("In AirStatee");
>>>>>>> 2530c0655a5d19272e2570d7a60403da8165c159
        t += Time.deltaTime;
        if (!player.IIC.AirTime.b_airTime && t > 0.2f)
        {
            player.TransitionToState(player.groundState);
        }

<<<<<<< HEAD
        if (player.IIC.WallrunDetect.wallrunning)
        {
            player.TransitionToState(player.wallrunState);
        }

        if (PortalMain.staticIsTriggerd && t > 0.3f)
        {
            //Debug.Log("Ready to tranition to portalState");
            player.TransitionToState(player.portalState);
        }
=======
        if (player.IIC.wallrunning.wallrunning)
        {
            player.TransitionToState(player.wallrunState);
        }
>>>>>>> 2530c0655a5d19272e2570d7a60403da8165c159
    }

    public override void ExitState(Context player)
    {
        //Debug.Log("Exiting Air State");
        //player._script2.enabled = false;
    }
}
