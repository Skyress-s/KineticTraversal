using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context : MonoBehaviour
{
    public GameObject playerGO;

    public InputInfoCenter IIC;

    public Wallrunning Wallrun2;

    public static PortalMain connectedPortal;

    public bool debugState;

    private PlayerBaseState currentState;

    public readonly PlayerGroundState groundState = new PlayerGroundState();
    public readonly PlayerSlideState slideState = new PlayerSlideState();
    public readonly PlayerAirState airState = new PlayerAirState();
    public readonly PlayerWallrunState wallrunState = new PlayerWallrunState();
    public readonly PlayerPortalState portalState = new PlayerPortalState();

    [Header("State Stack")]
    public string[] stateStack;

    private void Start()
    {
        stateStack = new string[10];

        //initialize the fist state
        SetInitalState(airState);
        void SetInitalState(PlayerBaseState state)
        {
            currentState = groundState;
            currentState.EnterState(this);
        }
    }

    public void TransitionToState(PlayerBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        //

        void AddToStack()
        {

        int ssl = stateStack.Length; //just the length of the stack
        for (int i = 0; i < stateStack.Length; i++)
        {
            if (i < ssl-1)
            {
                stateStack[ssl - i - 1] = stateStack[ssl - i - 2];
            }
            else
            {
                stateStack[0] = currentState.ToString();
            }
        }

        // compiling it to a string
        string compiled = "";
        for (int i = 0; i < stateStack.Length; i++)
        {
            compiled += ", " + stateStack[i];
        }
        //Debug.Log(compiled);
        //
        }

        AddToStack();

        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.Update(this);
        currentState.DebugState(this);
        //Debug.Log(currentState);
    }
}
