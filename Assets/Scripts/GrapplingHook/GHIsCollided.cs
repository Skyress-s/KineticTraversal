using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GHIsCollided : MonoBehaviour
{
    public bool _isColided;

    public GrapplingHookStates States;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _isColided = false;
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        _isColided = true;

    }

    /*private void OnCollisionExit(Collision collision)
    {
        _isColided = false;   
    }*/

    //dosent work becouse as soon as it hits it turn kinematic, makes somthing else
    



}
