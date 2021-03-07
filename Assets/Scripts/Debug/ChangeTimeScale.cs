using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeTimeScale : MonoBehaviour
{
    private float t;

    private Keyboard kb;

      // Start is called before the first frame update
    void Start()
    {
        t = 1f;
        kb = InputSystem.GetDevice<Keyboard>();
    }

    // Update is called once per frame
    void Update()
    {
        float interval = 0.2f;
        if (kb.oKey.wasPressedThisFrame)
        {
            t += interval;
            SetNewTimeScale();
        }
        if (kb.pKey.wasPressedThisFrame)
        {
            t -= interval;
            SetNewTimeScale();
        }

        if (t < 0.01f)
        {
            t = 0.1f;
        }
    }

    void SetNewTimeScale()
    {
        if (t < 0.01f)
        {
            t = 0.1f;
        }
        Time.timeScale = t;
    }
}
