using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallrunWallDetect : MonoBehaviour
{
    [SerializeField]
    private float rayDistance;

    public GameObject CameraObj;

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
    private void Update()
    {
        WallDetectionV2(true);
        WallDetectionV2(false);
    }
}
