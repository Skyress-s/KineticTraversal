using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirStateScript : MonoBehaviour
{
    public class AirState : PlayerState
    {
        public override void Enter()
        {
            Debug.Log("Enter Airstate");
        }

        public override void Exit()
        {
            //release grab joint
        }

        public override void Update()
        {
            Debug.Log("Update...");
        }
        public override void FixedUpdate()
        {
            Debug.Log("FixedUpdate...");
        }
    }

}