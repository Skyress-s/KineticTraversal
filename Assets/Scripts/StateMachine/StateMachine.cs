using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public PlayerState activeState;

    private void Start()
    {
        State currentState = GetNextState(State.Locked, Input.Open);
    }

    private void Update()
    {

        if (UnityEngine.Input.GetKeyDown(KeyCode.Space)) {
            //gjør dette om til en funksjon :^)
            activeState.Exit();
            activeState = new AirState();
            activeState.Enter();
        }
        activeState.Update();  
    }

    private void FixedUpdate()
    {
        activeState.FixedUpdate();
    }

    enum State { Open, Closed, Locked }
    enum Input { Open, Close, Lock, Unlock }
    static State GetNextState(State current, Input input)
    {
        if (current == State.Closed && input == Input.Open)
        {
            return State.Open;
        }
        else if (current == State.Open && input == Input.Close)
        {
            return State.Closed;
        }
        else if (current == State.Closed && input == Input.Lock)
        {
            return State.Locked;
        }
        else if (current == State.Locked && input == Input.Unlock)
        {
            return State.Closed;
        }

        throw new NotSupportedException($"{current} has no transition on {input}");
    }

    static void ChangeState(State activeState)
    {

    }

    public abstract class PlayerState
    {
        public abstract void Enter();

        public abstract void Exit();

        public abstract void Update();

        public abstract void FixedUpdate();
    }

    public class WallrunningState : PlayerState
    {
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
        public override void Enter()
        {
            //Play jump animation
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

    public class Ground : PlayerState
    {
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


