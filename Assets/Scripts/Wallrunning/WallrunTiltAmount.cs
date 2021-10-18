using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallrunTiltAmount : MonoBehaviour
{
    public float animationTilt;

    private float scriptTilt;

    public Transform TiltGameobject;

    public Wallrunning wallrun;

    private RaycastHit bufferhit;

    public InputInfoCenter IIC;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (wallrun.globalHit.normal.sqrMagnitude > 0.1f)
        {
            bufferhit = wallrun.globalHit;
        }

        if (IIC.grapplingHookStates.currentState != GrapplingHookStates.GHStates.hooked) {
            TiltGameobject.transform.up = (bufferhit.normal * animationTilt + Vector3.up * 70).normalized;
        }

    }
}
