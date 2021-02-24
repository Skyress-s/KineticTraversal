using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCrystals : MonoBehaviour
{
    public float moveDistance = 3f;

    public float periodTime = 3f;

    private Vector3 startPosition;

    private float randomStart;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        randomStart = Random.Range(0, 50);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPosition + transform.parent.transform.up * Mathf.Sin(Time.time * (1/periodTime) + randomStart) * moveDistance;
    }
}
