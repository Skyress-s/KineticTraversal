using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;
using UnityEngine.InputSystem;

public class MilkShakerShaker : MonoBehaviour
{
    public ShakeParameters PortalShake;

    public ShakeParameters HookRetracedShake;

    public Shaker ShakeManager;


    //input
    private Keyboard kb;

    // Start is called before the first frame update
    void Start()
    {
        kb = InputSystem.GetDevice<Keyboard>();

        PlayerPortalState.EnterPortalEvent += StartPortalShake;
        PlayerPortalState.ExitPortalEvent += StopPortalShake;

        GrapplingHookStates.hookRetractedEvent += StartHookRetractedShake;
    }

    // Update is called once per frame
    void Update()
    {
        if (kb.hKey.wasPressedThisFrame)
        {
            ShakeManager.Shake(HookRetracedShake);
        }       
    }

    private void StartHookRetractedShake()
    {
        ShakeManager.Shake(HookRetracedShake);
    }


    private void StartPortalShake()
    {
        StartCoroutine(WaitAndStartPortalShake());
        
    }

    private void StopPortalShake()
    {
        StopAllCoroutines();
        if (SP == null)
        {
            return;
        }

        SP.Stop(0.1f, true);
    }
    private ShakeInstance SP;
    private IEnumerator WaitAndStartPortalShake()
    {
        var f = PlayerPortalState.maxduration - (PortalShake.FadeIn);
        yield return new WaitForSeconds(f);
        SP = ShakeManager.Shake(PortalShake);
    }

    private void OnDisable()
    {
        PlayerPortalState.EnterPortalEvent -= StartPortalShake;
        PlayerPortalState.ExitPortalEvent -= StopPortalShake;

        GrapplingHookStates.hookRetractedEvent -= StartHookRetractedShake;
    }
}