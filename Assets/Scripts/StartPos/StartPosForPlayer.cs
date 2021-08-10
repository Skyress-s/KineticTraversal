using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosForPlayer : MonoBehaviour
{
    public static Vector3 startPos;
    public static Quaternion startRot;

    public static Vector3 rot;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }


    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawFrustum(Vector3.zero, 60f, 15f, 3f, 1f);
    }
}