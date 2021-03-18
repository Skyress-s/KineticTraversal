using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : StateMachineSeb.PlayerState
{
    public SlideState(GameObject gameObject) : base(gameObject)
    {
    }

    public override string GetName() => "Slide";
    
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