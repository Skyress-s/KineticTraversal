using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public Animator transition;

    public GameObject Image;
    // Start is called before the first frame update
    void Start()
    {

        Image.SetActive(true);
        transition.updateMode = AnimatorUpdateMode.Normal; //changes the update mode to normal, so the transition waits for the game to load and not skip   

        LevelManager.OnLevelEnd += EndLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EndLevel()
    {
        transition.updateMode = AnimatorUpdateMode.UnscaledTime; // changes to unscaled, to it plays a the correct speed
        Debug.Log("LevelIsEnding");
        transition.SetTrigger("Start");
        LevelManager.OnLevelEnd -= EndLevel;
    }
}
