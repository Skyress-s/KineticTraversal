using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeOutText : MonoBehaviour
{
    public TMP_Text text;

    public AnimationCurve alphaCurve;

    public float duration;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.alpha = alphaCurve.Evaluate(Time.timeSinceLevelLoad * 1 / duration);
    }
}
