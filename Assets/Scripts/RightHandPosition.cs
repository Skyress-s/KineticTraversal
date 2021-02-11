using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandPosition : MonoBehaviour
{
    public Transform CamRot;

    private Vector3 dirLastFrame;

    private Vector3 startPos;
    
    void Start()
    {
        startPos = transform.localPosition;
    }

    void LateUpdate()
    {
        // y rotation for the lerp
        var forward = transform.forward;

        var yforward = forward;
        yforward.y = 0f;

        var ydlf = dirLastFrame;
        ydlf.y = 0f;

        var yangle = Vector3.SignedAngle(yforward, ydlf, Vector3.up);
        //Debug.Log(var2);
        yangle *= 1 / 10f;

        // x rotation for the lerp
        //var xforward = forward;
        //xforward.x = 0f;

        //var xdlf = dirLastFrame;
        //xdlf.x = 0f;

        //var xyangle = Vector3.SignedAngle(xforward, xdlf, Vector3.left);

        //Debug.Log(xyangle);

        var xforward = forward.y;

        var lff = dirLastFrame.y;

        var ydelta = lff - xforward;

        ydelta *= 7f;

        //Debug.Log(ydelta);


        //translates the pos based on var
        Vector3 p = transform.localPosition;
        p.x = Mathf.Lerp(p.x, yangle + startPos.x, 10f * Time.deltaTime);
        p.y = Mathf.Lerp(p.y, ydelta + startPos.y, 10f * Time.deltaTime);


        
        transform.localPosition = p;


        //updates the degree for the next frame of rendering
        dirLastFrame = transform.forward;

    }
}
