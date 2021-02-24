using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTimeScale : MonoBehaviour
{
    private float t;
    // Start is called before the first frame update
    void Start()
    {
        t = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        float interval = 0.2f;
        if (Input.GetKeyDown(KeyCode.O))
        {
            t += interval;
            SetNewTimeScale();
        }
        if (Input.GetKeyDown(KeyCode.P))
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
        Time.timeScale = t;
    }
}
