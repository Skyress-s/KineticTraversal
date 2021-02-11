using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.transform.position + new Vector3(0f, 1f, 0f);

        //100% regel:
        //hvis noe er rart, øk output med hundre prosent
        //      
    }
}
