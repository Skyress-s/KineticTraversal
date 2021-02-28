using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    public InputInfoCenter IIC;

    public bool sliding;

    private CapsuleCollider cc;
    private float orgHeight;


    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CapsuleCollider>();
        orgHeight = cc.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.C))/* && infoCenterData.grounded._isgrounded*/
        {
            sliding = true;
            cc.height = 1f;
            cc.center = new Vector3(0f, 0.3f, 0f);
        }
        else
        {
            sliding = false;
            cc.height = orgHeight;
            cc.center = new Vector3(0f, 0, 0f);
        }
    }
}
