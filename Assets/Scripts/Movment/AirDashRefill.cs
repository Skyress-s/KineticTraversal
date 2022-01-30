using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AirDashRefill : MonoBehaviour
{
    enum RefillMode
    {
        Add,
        Set
    }

    [SerializeField]
    private RefillMode currentRefillMode;

    public int dashesToRefill = 3;

    [Header("Cooldown")]
    public float cooldown = 3;

    public VisualEffect VE;


    private void OnTriggerEnter(Collider other) {
        AirDash AD = other.gameObject.GetComponent<AirDash>();

        if (AD != null)
        {
            switch (currentRefillMode)
            {
                case RefillMode.Add:
                    AD.SetDasheseLeft(dashesToRefill, true);
                   
                    break;
                case RefillMode.Set:
                    AD.SetDasheseLeft(dashesToRefill, false);
                    
                    break;
                default:
                    break;
            }

            StartCoroutine(CooldownCoroutine());

            if (VE != null)
            {
                VE.Play();
                Vector3 toPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().velocity;
                toPlayer.Normalize();
                toPlayer *= 100;
                VE.SetVector3("Direction", toPlayer);
            }
        }

        
    }


    IEnumerator CooldownCoroutine()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(cooldown);
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<SphereCollider>().enabled = true;
    }

}
