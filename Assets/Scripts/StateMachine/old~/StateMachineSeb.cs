using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateMachineSeb : MonoBehaviour
{
    public PlayerState wallRunState, airState, groundState, slideState;
    public PlayerState activeState;

    public Dictionary<string, dynamic> scriptDict;

    public InputInfoCenter IIC;

    public GroundMovment_ForceVer groundMov;

    public StateMachineSeb FSM;

    private void Start()
    {
        FSM = gameObject.GetComponent<StateMachineSeb>();

        scriptDict = new Dictionary<string, dynamic>();

        scriptDict.Add("ground", IIC.grounded.gameObject.GetComponent<GroundMovment_ForceVer>());
        scriptDict.Add("air", IIC.grounded.gameObject.GetComponent<AirMovment>());
        
        

        wallRunState = new WallrunState(gameObject, scriptDict, FSM);
        airState = new AirState(gameObject, scriptDict, FSM);
        groundState = new GroundState(gameObject, scriptDict, FSM);
        //slideState = new SlideState(gameObject);


        activeState = groundState;
    }

    public bool grounded;
    public bool air;
    public bool wallrun;
    public bool slide;


    private void Update()
    {
        //Gets the bools for what is active
        //priority Slide > ground > Wallrun > Air

        grounded = IIC.grounded._isgrounded;
        air = IIC.AirTime.b_airTime;
        slide = IIC.infoSliding.sliding;
        wallrun = IIC.wallrunning.wallrunning;

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

    public void ChangeState(PlayerState next)
    {
        activeState.Exit();
        activeState = next;
        activeState.Enter();
    }

    public abstract class PlayerState
    {
        protected GameObject gameObject;

        protected Dictionary<string, dynamic> dictonairy;

        protected StateMachineSeb FSM;
        public PlayerState(GameObject gameObject, Dictionary<string, dynamic> inputDictonairy, 
            StateMachineSeb inputFSM)
        {
            this.gameObject = gameObject;
            dictonairy = inputDictonairy;
            FSM = inputFSM;
        }


        //public PlayerState(GameObject gameObject, Dictionary<string, dynamic> dictonairy)
        //{
        //    this.gameObject = gameObject;
        //}

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
        Dictionary<string, dynamic> dictonairy;

        StateMachineSeb FSM;

        public WallrunState(GameObject gameObject, Dictionary<string, dynamic> inputDictionary, StateMachineSeb inputFSM) 
            : base(gameObject, inputDictionary, inputFSM)
        {
            dictonairy = inputDictionary;
            FSM = inputFSM;
        }
        //public WallrunState(GameObject gameObject, Dictionary<string, dynamic> d) : base(d) { }


        public override string GetName() => "Wallrunning222";
        
        public override void Enter()
        {
            //Play wallrun animation
            //Debug.Log(gameObject.name + dictonairy);
        }

        public override void Exit()
        {
            //release grab joint
            FSM.ChangeState(FSM.wallRunState);
        }

        public override void Update()
        {
            //play wallrun particles
            var air = dictonairy["air"];
            air.enabled = false;
        }
        public override void FixedUpdate()
        {
            //play wallrun particles
        }
    }

    public class AirState : PlayerState
    {
        Dictionary<string, dynamic> dictonairy;

        StateMachineSeb FSM;

        public AirState(GameObject gameObject, Dictionary<string, dynamic> inputDictionary, StateMachineSeb inputFSM) 
            : base(gameObject, inputDictionary, inputFSM)
        {
            dictonairy = inputDictionary;
            FSM = inputFSM;
        }
        //public WallrunState(GameObject gameObject, Dictionary<string, dynamic> d) : base(d) { }


        public override string GetName() => "AirState";
        
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
            //var air = dictonairy["air"];
            //air.enabled = false;
            if (!FSM.air)
            {
                FSM.ChangeState(FSM.groundState);
            }
        }
        public override void FixedUpdate()
        {
            //play wallrun particles
        }
    }

    public class GroundState : PlayerState
    {
        Dictionary<string, dynamic> dictonairy;

        StateMachineSeb FSM;

        public GroundState(GameObject gameObject, Dictionary<string, dynamic> inputDictionary, StateMachineSeb inputFSM) 
            : base(gameObject, inputDictionary, inputFSM)
        {
            dictonairy = inputDictionary;
            FSM = inputFSM;
        }
        //public WallrunState(GameObject gameObject, Dictionary<string, dynamic> d) : base(d) { }


        public override string GetName() => "GroundState";
        
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
            if (FSM.air)
            {
                FSM.ChangeState(FSM.airState);
            }
        }
        public override void FixedUpdate()
        {
            //play wallrun particles
        }
    }
}


