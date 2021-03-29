using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMain : MonoBehaviour
{
    public bool isTriggerd;

    public static bool staticIsTriggerd;

    public static Vector3 centerOfPortal;

    public GameObject[] PortalGO;

    private Transform cameraT;

    private enum State {dorment, active, inactive}

    [SerializeField]
    private State currentState;

    public Quaternion onPlayRotation;

    private float t;

    public float cooldown;

    private void Update()
    {
        switch (currentState)
        {
            case State.dorment:
                DormantState();
                break;
            case State.active:
                ActiveState();
                break;
            case State.inactive:
                InactiveState();
                break;
        }
    }

    private void ActiveState()
    {

        var newrot = Quaternion.LookRotation(-cameraT.forward, -Vector3.up);

        //PortalGO[0].transform.rotation = Quaternion.Slerp(PortalGO[0].transform.rotation, newrot, Time.deltaTime * 2f);
        GetNewRot(0, 2f);
        GetNewPos(0, 5f, 4f);

        //PortalGO[1].transform.rotation = Quaternion.Slerp(PortalGO[1].transform.rotation, newrot, Time.deltaTime * 1.5f);
        GetNewRot(1, 1.5f);
        GetNewPos(1, 2.5f, 3f);

        //PortalGO[2].transform.rotation = Quaternion.Slerp(PortalGO[2].transform.rotation, newrot, Time.deltaTime * 1f);
        GetNewRot(2, 1f);
        GetNewPos(2, 1f, 2f);


        void GetNewRot(int i, float lerpTime)
        {
            PortalGO[i].transform.rotation = Quaternion.Slerp(PortalGO[i].transform.rotation, newrot, lerpTime * Time.deltaTime);
        }


        void GetNewPos(int i, float distance, float lerpTime)
        {
            PortalGO[i].transform.position = Vector3.Slerp(PortalGO[i].transform.position, transform.position + cameraT.forward * distance,
                lerpTime * Time.deltaTime);
        }

    }
    
    private void DormantState()
    {
        ResetRotPos(0, 5f, 10f);
        ResetRotPos(1, 4f, 7f);
        ResetRotPos(2, 3f, 5f);

        Debug.Log("Dormant");
        void ResetRotPos(int i, float rotLerp, float posLerp)
        {
            PortalGO[i].transform.rotation = Quaternion.Slerp(PortalGO[i].transform.rotation, onPlayRotation, rotLerp * Time.deltaTime);
            PortalGO[i].transform.localPosition = Vector3.Lerp(PortalGO[i].transform.localPosition, Vector3.zero, posLerp * Time.deltaTime);
        }

        t += Time.deltaTime;

        if (t > cooldown) //time to wait before jumping to inactive state
        {
            t = 0f;
            currentState = State.inactive;
        }
    }
    
    private void InactiveState()
    {
        //Do nothing, is inactive
    }

    private void Start()
    {
        currentState = State.inactive;
        cameraT = Camera.main.transform;
        onPlayRotation = PortalGO[0].transform.rotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (currentState != State.inactive) return; 
        OnEnter();
        isTriggerd = true;
        staticIsTriggerd = true;
    }
    private void OnTriggerExit(Collider other)
    {
        OnExit();
        isTriggerd = false;
        staticIsTriggerd = false;
    }

    private void OnEnter()
    {
        DisableEnablePortalColliders(false);
        centerOfPortal = transform.position;
        if (currentState == State.inactive)
        {
            currentState = State.active;
        }
    }

    private void OnExit()
    {
        DisableEnablePortalColliders(true);
        currentState = State.dorment;
    }

/// <summary>
/// 
/// </summary>
/// <param name="b"> b = true -> enable</param>
    private void DisableEnablePortalColliders(bool b)
    {
        for (int i = 0; i < PortalGO.Length; i++)
        {
            PortalGO[i].GetComponent<MeshCollider>().enabled = b;
        }

    }
}
