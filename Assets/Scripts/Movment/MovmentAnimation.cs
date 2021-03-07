﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentAnimation : MonoBehaviour
{
    public Animator Animator;

    public IsGrounded grounded;

    public Wallrunning WallrunningScript;

    public InputInfoCenter IIC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var x = IIC.input.x;
        var y = IIC.input.y;

        var c = Mathf.Abs(x) + Mathf.Abs(y);
        //Debug.Log(c);
        var margin = 0.1f;

        //checks if player i walking
        if (c > margin)
        {
            Animator.SetBool("Walk", true);
        }
        //if player is stand still 
        if (c < margin)
        {
            Animator.SetBool("Walk", false);
        }

        //if grounded and running
        if (IIC.holdSprint && grounded._isgrounded)
        {
            //Animator.SetBool("Run", true);
            Animator.speed = 2;
        }

        //is not running
        else if (IIC.holdSprint == false || grounded._isgrounded == false)
        {
            Animator.speed = 1;
        }

        //jumping
        if (grounded._isgrounded == true)
        {
            Animator.SetBool("Jump", false);
        }
        //not jumping
        else if (grounded._isgrounded == false)
        {
            Animator.SetBool("Jump", true);
        }

        //wallrundetection
        if (WallrunningScript.wallrunning == true)
        {
            Animator.SetBool("Wallrun", true);
        }
        else
        {
            Animator.SetBool("Wallrun", false);
        }
        
    }
}
