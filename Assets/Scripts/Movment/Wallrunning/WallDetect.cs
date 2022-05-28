using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WallDetect : MonoBehaviour
{
    [SerializeField] private FDetectConfig DetectConfig;
    
    public FDetectState DetectState { get; private set;
    }

    private int layerMask;

    
    public UnityEvent EFoundWall;

    public UnityEvent ELostWall;

    private void Start() {
        string[] layers = { "Player, Weapon" };
        layerMask = LayerMask.GetMask(layers);
        layerMask = ~layerMask;
    }

    private void FixedUpdate() {
        DoWallDetect();
    }

    private void DoWallDetect() {
        List<RaycastHit> _hits = new List<RaycastHit>();
        int rays = DetectConfig.raySegments;
        for (int i = 0; i < rays; i++) {
            Vector3 _location = transform.position;
            Vector3 _forwards = transform.forward * Mathf.Sin(2f * Mathf.PI * ((float)i / (float)rays));
            Vector3 _right = transform.right * Mathf.Cos(2f * Mathf.PI * ((float)i / (float)rays));
            Vector3 _direction = _forwards + _right;
            RaycastHit hit;
            if (Physics.Raycast(_location, _direction, out hit, DetectConfig.rayLength, layerMask, QueryTriggerInteraction.Ignore)) {
                if (KineticTags.IsWallrunable(hit.transform.gameObject)) 
                    _hits.Add(hit);
            }
            Debug.DrawRay(_location, _direction * DetectConfig.rayLength, Color.blue);
        }
        
        Vector3 _avgDirection = new Vector3();
        
        foreach (RaycastHit hit in _hits) {
            _avgDirection += hit.normal;
        }
        _avgDirection /= (float)_hits.Count;

        FDetectState state = new FDetectState();
        
        if (_hits.Count > 0) {
            state.Hit = true;
            state.Direction = _avgDirection;
            EFoundWall?.Invoke();
 
        }
        else {
            state.Hit = false;
            state.Direction = Vector3.zero;
            ELostWall?.Invoke();
            // Debug.Log("LostWall");
        }

        DetectState = state;


    }
    
    [Serializable]
    public struct FDetectConfig {
        public int raySegments;
        public float rayLength;
    }
    
    public struct FDetectState {
        public bool Hit;
        public Vector3 Direction;
    }
}
