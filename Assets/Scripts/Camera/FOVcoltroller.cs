using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVcoltroller : MonoBehaviour
{
    public Camera camera;

    private float startFOV;

    public AnimationCurve hookedCurve;

    [Header("hooked FOV effect")]
    public float amplitude, duration;


    private float t;
    // Start is called before the first frame update
    void Start()
    {
        GrapplingHookStates.hooked += hookedFOVeffect;
        startFOV = camera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        camera.fieldOfView = startFOV + amplitude * hookedCurve.Evaluate(t/duration);
    }

    private void hookedFOVeffect()
    {
        t = 0f;
    }
}
