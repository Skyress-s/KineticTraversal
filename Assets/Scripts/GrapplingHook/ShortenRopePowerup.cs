using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // den følelsen når du glemmer å skrive ned denne i starten og må rive ut håret i 1 time før du finner ut av det. rip in pepperinos
public class ShortenRopePowerup : MonoBehaviour
{
    public GrapplingHook GH;

    public GameObject PowerUpImage;

    public float sensetivity;

    public float guage;

    private float n;

    public InputInfoCenter IIC;

    
    // Start is called before the first frame update
    void Start()
    {
        n = guage;
    }

    // Update is called once per frame
    void Update()
    {
        //reset guage is you hit ground 
        if (IIC.grounded._isgrounded)
        {
            guage = n;
        }

        if (IIC.grapplingHookStates.currentState.ToString() == "hooked")
        {
            
            if (guage > 0)
            {
                //shortenRope
                if (IIC.holdShortenRope)
                {
                    ShortenRope();
                }

                // Lengthen rope
                if (IIC.holdLengthenRope)
                {
                    LengthenRope();
                }

            }
        }
        
        //reset counter if it touches ground;

        //powerupImage render
        var var = guage / n;

        PowerUpImage.GetComponent<Image>().fillAmount = var;
    }
    
    void ShortenRope()
    {
        // Shorten rope
        
        GH.originalDist += -sensetivity * Time.deltaTime;
        guage -= Time.deltaTime;
        
    }

    void LengthenRope()
    {
        GH.originalDist += sensetivity * Time.deltaTime;
        guage -= Time.deltaTime;
    }
    void MouseWheelShoreten()  //sensetivity = 50
    {
        float wheel = Input.mouseScrollDelta.y;
        wheel = Mathf.Abs(wheel);
        wheel = Mathf.Clamp(wheel, 0, 1);
        wheel = wheel * Time.deltaTime;

        Debug.Log(wheel);


        GH.originalDist -= wheel * sensetivity;
    }
}
