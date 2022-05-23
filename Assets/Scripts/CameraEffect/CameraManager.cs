using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private PostProcessEffect _airDashPostProcessEffect;
    [SerializeField] private PostProcessEffect _resetplayerPostProcessEffect;


    [SerializeField] private AirDash _airDash; 

    private Keyboard _keyboard;
    public static CameraManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

    private void Start()
    {
        _keyboard = InputSystem.GetDevice<Keyboard>();
        
        //subscribe to events
        AirDash.EDashesChanged += (int d) => {
            if (_airDash.maxDashes != d) 
                PlayAirDashPostProcessEffect(); 
        };

        DebugResetPlayer.ERestartLevelEvent += PlayResetPlayerPostProcessEffect;

    }

   

    private void Update()
    {
        if (_keyboard.vKey.wasPressedThisFrame)
        {
            PlayAirDashPostProcessEffect();
        }
    }

    private void PlayAirDashPostProcessEffect() {
        PlayPostProcessEffect(ref _airDashPostProcessEffect);
    }

    private void PlayResetPlayerPostProcessEffect() {
        PlayPostProcessEffect(ref _resetplayerPostProcessEffect);
    }
    
    
    public void PlayPostProcessEffect(ref PostProcessEffect _postProcessEffect) {
        Debug.Log("Play camera effect");
        Volume volumeComp = gameObject.AddComponent<Volume>();
        volumeComp.weight = 0f;
        volumeComp.isGlobal = true;
        volumeComp.profile = _postProcessEffect.PostProcessProfile;
        StartCoroutine(IPlayPostProcessEffect(_postProcessEffect, volumeComp));
    }

    IEnumerator IPlayPostProcessEffect(PostProcessEffect _postProcessEffect, Volume _volumeComp) {
        float _time = 0f;
        while (_time < _postProcessEffect.Duration) {
            yield return new WaitForEndOfFrame();
            _time += Time.deltaTime;
            _volumeComp.weight = _postProcessEffect.Curve.Evaluate(_time / _postProcessEffect.Duration);
        }
        Destroy(_volumeComp);
    }
}



// [Serializable]
// public class PostProcessProfileEffect
// {
//     public PostProcessProfileEffect()
//     {
//         duration = 1f;
//     }
//     public float duration = 1f;
//     public AnimationCurve curve;
//     public VolumeProfile profile;
// }
