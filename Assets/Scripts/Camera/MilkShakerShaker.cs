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
   

    private void OnDisable()
    {
        GrapplingHookStates.hookRetractedEvent -= StartHookRetractedShake;
    }
}
