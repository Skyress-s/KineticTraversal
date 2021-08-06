using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FOVcoltroller : MonoBehaviour
{
    public Camera camera;

    private float startFOV;

    public AnimationCurve hookedCurve;

    public AnimationCurve hookRetractedCurve;

    [Header("hooked FOV effect")]
    public float amplitude, duration;

    public List<ReadCurve> curves;

    public List<int> intergers;


    private float t;


    public Keyboard kb;


    public ReadCurve curveread;
    // Start is called before the first frame update
    void Start()
    {
        kb = InputSystem.GetDevice<Keyboard>();

        //curves.Add(new ReadCurve(hookedCurve, 0.3f, 10f));


        GrapplingHookStates.hooked += hookedFOVeffect;
        startFOV = camera.fieldOfView;

        curveread = new ReadCurve(hookRetractedCurve, 2f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        //camera.fieldOfView = startFOV + amplitude * hookedCurve.Evaluate(t/duration);

        //camera.fieldOfView = startFOV + curveread.curveValue;

        if (kb.bKey.wasPressedThisFrame)
        {
            hookedFOV();
        }
    }
    
    private void hookedFOV()
    {

        //curves.Add(curveread);
        intergers.Add(2);
    }

    private void hookedFOVeffect()
    {
        t = 0f;
    }
}


public class ReadCurve
{
    private float t;


    private float _backerCurveValue;
    public float curveValue
    {
        get{ 
            UpdateCurve();
            return _backerCurveValue;
        }
        set{ _backerCurveValue = value; }
    }

    public AnimationCurve curve;
    public float duration, amplitude;
    public ReadCurve(AnimationCurve _curve, float _duration, float _amplitude)
    {
        curve = _curve;
        duration = _duration;
        amplitude = _amplitude;
    }

    public void UpdateCurve()
    {
        t += Time.deltaTime;
        curveValue = amplitude * curve.Evaluate(t/duration);
    }
}
