using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallrunTiltAmount : MonoBehaviour
{
    public float animationTilt;

    private float scriptTilt;

    public Transform TiltGameobject;

    public Wallrunning wallrun;

    private RaycastHit bufferhit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (wallrun.globalHit.normal.sqrMagnitude > 0.1f)
        {
            bufferhit = wallrun.globalHit;
        }

        //scriptTilt = animationTilt * wallrun.DetermineSide();
        //TiltGameobject.transform.localEulerAngles = new Vector3(0, 0, scriptTilt);

        TiltGameobject.transform.up = (bufferhit.normal * animationTilt + Vector3.up * 70).normalized;
        


        //New method

        //HAHAHAHAHAHH DET FUNKA SEBASTIAN!!!!! <3!!!!!!!! HAHAH SÅ MANGE PROBLEMER LØST!
    }
}
