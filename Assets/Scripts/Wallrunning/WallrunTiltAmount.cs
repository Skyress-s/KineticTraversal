using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallrunTiltAmount : MonoBehaviour
{
    public float animationTilt;

    public float scriptTilt;

    public Transform TiltGameobject;

    public Wallrunning wallrun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scriptTilt = animationTilt * wallrun.DetermineSide();
        TiltGameobject.transform.localEulerAngles = new Vector3(0, 0, scriptTilt);

        //HAHAHAHAHAHH DET FUNKA SEBASTIAN!!!!! <3!!!!!!!! HAHAH SÅ MANGE PROBLEMER LØST!
    }
}
