using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script2 : MonoBehaviour
{
    private void OnDisable()
    {
        transform.position += Vector3.up;
    }

    private void OnEnable()
    {
        transform.position += -Vector3.up;
    }
}
