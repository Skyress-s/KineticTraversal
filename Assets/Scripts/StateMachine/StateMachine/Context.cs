using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context : MonoBehaviour
{
    public GameObject playerGO;

    public InputInfoCenter IIC;

    public Wallrunning Wallrun2;


    private PlayerBaseState currentState;

    public readonly PlayerGroundState groundState = new PlayerGroundState();
    public readonly PlayerSlideState slideState = new PlayerSlideState();
    public readonly PlayerAirState airState = new PlayerAirState();
    public readonly PlayerWallrunState wallrunState = new PlayerWallrunState();
    public readonly PlayerPortalState portalState = new PlayerPortalState();

    private void Start()
    {
        SetInitalState(groundState);
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
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.Update(this);

        //Debug.Log(currentState);
    }
}
