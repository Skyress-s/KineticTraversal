using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Call Updates() yourself
/// </summary>
public abstract class StateHandlerBase : MonoBehaviour {
   [SerializeField] public bool _bDebugState = true;
   [SerializeField] private int _pushDownAutomataMax = 5;
   [SerializeField] public List<BaseState> _stateStack { get; protected set; }

   public delegate void StateDelegate(BaseState newState);
   
   public event StateDelegate EEnterNewState; 

   public void EnterState(BaseState newState, float delay = 0f) {

      StartCoroutine(EnterDelay(1f, (state) => state = newState ));
      
      _stateStack[0].Exit();
      _stateStack.Insert(0, newState);
      _stateStack[0].Enter();
      
      
      
      EEnterNewState?.Invoke(_stateStack[0]);
      
      //clamps size
      if (_stateStack.Count >= _pushDownAutomataMax) {
         _stateStack.RemoveAt(_pushDownAutomataMax - 1);
      }
   }

   /// <summary>
   /// resets stack and adds the inutted state to the front
   /// </summary>
   /// <param name="_startState"></param>
   protected void Init(BaseState _startState) {
      _stateStack = new List<BaseState>(0);
      _stateStack.Add(_startState);
      _stateStack[0].Enter();
   }

   private IEnumerator EnterDelay(float time, System.Action<BaseState> car) {
      yield return new WaitForSeconds(time);
      
      

   }

}
