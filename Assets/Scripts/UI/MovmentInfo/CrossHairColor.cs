using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHairColor : MonoBehaviour
{
    public Image targetImage;

    public Camera camera;

    private int layermask;

    public Rigidbody playerRB;

    public GrapplingHookStates GHS;
    // Start is called before the first frame update
    void Start()
    {
        
        layermask = 1 << 9; // creates a bitmask, only hits playerlayer
        layermask = ~layermask; 
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 1000f, layermask, QueryTriggerInteraction.Ignore))
        {
            float raydistancecalc = GHS.hookMaxDistance + GHS.maxRayDistance + VelocityTowardsTarget(playerRB, hit.collider.transform);

            //Debug.Log(raydistancecalc);
            if ((hit.collider.gameObject.transform.position - camera.transform.position).sqrMagnitude < raydistancecalc * raydistancecalc)
            {
                if (!KineticTags.IsHookable(hit.collider.gameObject)) return;
                
                targetImage.color = Color.cyan;
            }
            else
            {
                targetImage.color = Color.white;
            }
        }
        else
        {
            targetImage.color = Color.white;
        }
    }

    /// <summary>
    /// Finds the velocity towards the target rigidbody
    /// </summary>
    /// <param name="rb"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public float VelocityTowardsTarget(Rigidbody rb, Transform target)
    {
        //need to know vector from rb to target and velocity vector
        Vector3 rbTOtarget = target.position - rb.transform.position;

        Vector3 velo = rb.velocity;

        //we also need the angle between them, not signed

        float theta = Vector3.Angle(rbTOtarget, velo);

        //ready to do some calculations
        float velTOtarget = Mathf.Cos(theta * Mathf.Deg2Rad) * velo.magnitude;

        //Debug.Log(velTOtarget);
        return velTOtarget;
    }
}
