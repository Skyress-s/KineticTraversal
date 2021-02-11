using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HookedVFX : MonoBehaviour
{
    [SerializeField]
    private VisualEffect visualEffect;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            visualEffect.Play();
        }
    }
}
