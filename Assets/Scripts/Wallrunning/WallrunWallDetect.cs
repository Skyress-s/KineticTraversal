﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallrunWallDetect : MonoBehaviour
{
    public bool wallrunning;

    public bool isRightSide;

    [SerializeField]
    private float rayDistance;

    public ColliderTrigger CT;

    public GameObject CameraObj;

    public RaycastHit globalHit;

    RaycastHit WallDetectionV2(bool b)
    {
        var right = 0;
        if (b == true) right = 1;
        else right = -1;

        var v = CameraObj.transform.right;
        v.y = 0f;
        v = v.normalized;


        Debug.DrawRay(transform.position, v * rayDistance * right, Color.green, Time.deltaTime);
        
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, v * right, out hit, rayDistance))
        {
            return hit;
        }
        else
        {
            return hit;
        }
    }
    
    public void DetectWall()
    {
        RaycastHit hit1 = WallDetectionV2(true);
        RaycastHit hit2 = WallDetectionV2(false);

        if (hit1.collider != null)
        {
            globalHit = hit1;
            isRightSide = true;
            wallrunning = true;
        }
        else if (hit2.collider != null)
        {
            globalHit = hit2;
            isRightSide = false;
            wallrunning = true;
        }
        else
        {
            globalHit = new RaycastHit();
            //isRightSide = false;
            wallrunning = false;
        }

        
    }

    private void FixedUpdate()
    {
        DetectWall();
    }
}
