using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityIndicator : MonoBehaviour
{
    public Rigidbody rb;

    public InputInfoCenter IIC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IIC.AirTime.b_airTime)
        {
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
        else
        {
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
            Quaternion target = Quaternion.LookRotation(rb.velocity, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, target, 50f * Time.deltaTime);

            //length portion
            Vector3 newScale = new Vector3(1f, 1f, rb.velocity.magnitude/69f);
            transform.localScale = newScale;
        }
    }
}
