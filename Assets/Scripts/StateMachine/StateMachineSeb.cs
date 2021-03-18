using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateMachineSeb : MonoBehaviour
{
    public PlayerState wallRunState, airState, groundState, slideState;
    public PlayerState activeState;

    private void Start()
    {
        wallRunState = new WallrunningState(gameObject);
        airState = new AirState(gameObject);
        groundState = new Ground(gameObject);
        slideState = new SlideState(gameObject);

        activeState = groundState;
    }

    private void Update()
    {
        if (InputSystem.GetDevice<Keyboard>().spaceKey.wasPressedThisFrame){
            if(activeState == groundState)
            {
                ChangeState(airState);
            }
        }
        Debug.Log("Active state is: " + activeState.GetName());
        activeState.Update();
    }

    private void FixedUpdate()
    {
        activeState.FixedUpdate();
    }

    private void ChangeState(PlayerState next)
    {
        activeState.Exit();
        activeState = next;
        activeState.Enter();
    }

    public abstract class PlayerState
    {
        protected GameObject gameObject;
        public PlayerState(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
        public abstract string GetName();
        public abstract void Enter();

        public abstract void Exit();

        public abstract void Update();

        public abstract void FixedUpdate();
    }

    public class WallrunningState : PlayerState
    {
        public WallrunningState(GameObject gameObject) : base(gameObject)
        {
        }

        public override string GetName() => "Wallrunning";
        
        public override void Enter()
        {
            //Play wallrun animation
        }

        public override void Exit()
        {
            //release grab joint
        }

        public override void Update()
        {
            //play wallrun particles
        }
        public override void FixedUpdate()
        {
            //play wallrun particles
        }
    }

    public class AirState : PlayerState
    {
        public AirState(GameObject gameObject) : base(gameObject)
        {
        }

        public override string GetName() => "Air State";
        public override void Enter()
        {
            Debug.Log("Air state was entered");
        }

        public override void Exit()
        {
            //release grab joint
        }

        public override void Update()
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.one * -9.81f);
            Debug.Log("We are in the air!!!");
        }
        public override void FixedUpdate()
        {
            //play wallrun particles
        }
    }

    public class Ground : PlayerState
    {
        public Ground(GameObject gameObject) : base(gameObject)
        {
        }

        public override string GetName() => "Ground State";

        public override void Enter()
        {
            // On enter
        }

        public override void Update()
        {
            // On Update
        }

        public override void Exit()
        {
            // On Exit
        }
        public override void FixedUpdate()
        {
            //play wallrun particles
        }
    }
}


