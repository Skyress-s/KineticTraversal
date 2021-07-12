using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObsticleMove : MonoBehaviour
{

    public Transform Target;

    public float period;

    private float t;

    private Vector3 dir;

    private Vector3 startLocPos;

    // Start is called before the first frame update
    void Start()
    {
        startLocPos = transform.localPosition;
        dir = (Target.localPosition - transform.localPosition);
        t = -1f;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        var sin = Mathf.Sin(2 * Mathf.PI * t/period)/2f + 0.5f;
        transform.localPosition = dir * sin + startLocPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(Target.position, 2f);
    }
}
