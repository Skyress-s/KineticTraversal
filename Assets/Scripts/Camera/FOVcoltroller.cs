using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FOVcoltroller : MonoBehaviour
{
    public Camera camera;

    private float startFOV;

    [Header("hookedCurve")]
    public AnimationCurve hookedCurve;
    [SerializeField][Tooltip("shoudl be 2 elements, first is duratian, second is amplitude")]
    private float[] hookedCurveValues;

    [Header("RetractedCurve")]
    public AnimationCurve hookRetractedCurve;
    [SerializeField][Tooltip("shoudl be 2 elements, first is duratian, second is amplitude")]
    private float[] hookedRetractedCurveValues;


    public Keyboard kb;

    public ReadCurve[] readCurves;

    // Start is called before the first frame update
    void Start()
    {
        readCurves = new ReadCurve[2];
        for (int i = 0; i < readCurves.Length; i++)
        {
            readCurves[i] = new ReadCurve(hookedCurve, 0.5f, 0f);
        }

        kb = InputSystem.GetDevice<Keyboard>();

        //curves.Add(new ReadCurve(hookedCurve, 0.3f, 10f));


        GrapplingHookStates.hooked += hookedFOV;
        GrapplingHookStates.hookRetractedEvent += hookRetractedFOV;


        startFOV = camera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {

        if (kb.bKey.wasPressedThisFrame)
        {
            hookedFOV();
        }
        if (kb.nKey.wasPressedThisFrame)
        {
            hookRetractedFOV();
        }

        float val = 0f;
        for (int i = 0; i < readCurves.Length; i++)
        {
            val += readCurves[i].curveValue;
        }

        camera.fieldOfView = startFOV + val;
    }

    private void hookedFOV()
    {

        ReadCurve curve2 = new ReadCurve(hookedCurve, hookedCurveValues[0], hookedCurveValues[1]);
        readCurves[0] = curve2;

        
        
    }

    private void hookRetractedFOV()
    {
        ReadCurve retractedReadCurve = new ReadCurve(hookRetractedCurve, hookedRetractedCurveValues[0], hookedRetractedCurveValues[1]);
        readCurves[1] = retractedReadCurve;
    }
}


public class ReadCurve
{
    private float t;


    private float _backerCurveValue = 0f;
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
