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
                    AD.dashesLeft += dashesToRefill;
                    break;
                case RefillMode.Set:
                    AD.dashesLeft = dashesToRefill;
                    break;
                default:
                    break;
            }

            StartCoroutine(CooldownCoroutine());

            if (VE != null)
            {
                VE.Play();
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
