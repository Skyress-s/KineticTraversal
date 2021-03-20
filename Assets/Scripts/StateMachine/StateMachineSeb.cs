using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateMachineSeb : MonoBehaviour
{
    public PlayerState wallRunState, airState, groundState, slideState;
    public PlayerState activeState;

    public static Dictionary<string, dynamic> scriptDict;
    

    public InputInfoCenter IIC;

    public GroundMovment_ForceVer groundMov;

    private void Start()
    {
        //scriptDict.Add("ground", IIC.grounded.gameObject.GetComponent<GroundMovment_ForceVer>());
        //scriptDict.Add("air", IIC.grounded.gameObject.GetComponent<AirMovment>());

        

        wallRunState = new WallrunState(gameObject);
        airState = new AirState(gameObject);
        groundState = new GroundState(gameObject);
        slideState = new SlideState(gameObject);


        activeState = groundState;
    }

    private void Update()
    {
        //Gets the bools for what is active
        //priority Slide > ground > Wallrun > Air

        var grounded = IIC.grounded._isgrounded;
        var air = IIC.AirTime.b_airTime;
        var slide = IIC.infoSliding.sliding;
        var wallrun = IIC.wallrunning.wallrunning;

        //need refrences to the other scripts



        if (InputSystem.GetDevice<Keyboard>().spaceKey.wasPressedThisFrame){
            if(activeState == groundState)
            {
                ChangeState(wallRunState);
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

        //protected Dictionary<string, dynamic> dictonairy;

        //public PlayerState(Dictionary<string, dynamic> dic)
        //{
        //    this.dictonairy = dic;
        //}

        
        public abstract string GetName();
        public abstract void Enter();

        public abstract void Exit();

        public abstract void Update();

        public abstract void FixedUpdate();
    }

    public class WallrunState : PlayerState
    {
        public WallrunState(GameObject gameObject) : base(gameObject) { }
        //public WallrunState(GameObject gameObject, Dictionary<string, dynamic> d) : base(d) { }
        
        public override string GetName() => "Wallrunning";
        
        public override void Enter()
        {
            //Play wallrun animation
            //Debug.Log(gameObject.name + dictonairy);
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
            //gameObject.GetComponent<Rigidbody>().AddForce(Vector3.one * -9.81f);
            Debug.Log("We are in the air!!!");
        }
        public override void FixedUpdate()
        {
            //play wallrun particles
        }
    }

    public class GroundState : PlayerState
    {
        public GroundState(GameObject gameObject) : base(gameObject)
        {
        }

        

        public override string GetName() => "Ground State";

        public override void Enter()
        {
            // Enable GroundMov
            //disable everything else

            
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


    public class SlideState : PlayerState
    {
        public SlideState(GameObject gameObject) : base(gameObject)
        {
        }

        public override string GetName() => "SlideState";

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
}


