using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context : MonoBehaviour
{
    //Scripts to disable
    #region
    public Script1 _script1;
    public Script2 _script2;
    public Script3 _script3;
    #endregion


    public InputInfoCenter IIC;


    private PlayerBaseState currentState;

    public readonly PlayerGroundState groundState = new PlayerGroundState();
    public readonly PlayerSlideState slideState = new PlayerSlideState();
    public readonly PlayerAirState airState = new PlayerAirState();
    public readonly PlayerWallrunState wallrunState = new PlayerWallrunState();

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

        Debug.Log(currentState);
    }
}
