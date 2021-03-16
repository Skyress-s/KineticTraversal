using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GrapplingHookStates : MonoBehaviour
{   public enum GHStates
    {
        rest,
        fire,
        travling,
        hooked,
        returning,
        knockoff
    }

    public InputInfoCenter IIC;
    private Vector3 storedVelocity;
    public GHStates currentState;

    public Transform handPos;

    private Rigidbody rb;

    public GHIsCollided Collided;

    public float fireSpeed;

    //Ray Collision
    public float maxRayDistance;

    public RaycastHit globalHit;

    //Return d for distance to hand
    private float d;
    [Space]
    public float hookMaxDistance;


    //layermask
    private int layerMask;

    //visual effekt
    [Header("VFX")]
    [SerializeField][Space]
    private VisualEffect visualEffect;
    public HookVFX hookVFX;

    //animation
    [Header("Animation")]
    [Space]
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Collided = GetComponent<GHIsCollided>();

        
        //some layer mask magic, SEBSELEB EXPLAIN
        layerMask = 1 << 8;
        layerMask = ~layerMask; // inverts the bitmask

        onEnter = true;
    }

    

    // Update is called once per frame
    void LateUpdate()
    {
        

        switch (currentState)
        {
            case (GHStates.rest):
                IsResting();
                break;
            case (GHStates.fire):
                IsFireing();
                break;
            case (GHStates.travling):
                IsTraveling();
                break;
            case (GHStates.hooked):
                IsHooked();
                break;
            case (GHStates.returning):
                IsReturning();
                break;
            case (GHStates.knockoff):
                KnockOff();
                break;
        }

    }
    void IsResting()
    {
        //sets the transform of kinematic for resting pos
        transform.position = handPos.transform.position;
        transform.rotation = handPos.transform.rotation;
        rb.isKinematic = true;

        //check if the player fires
        if (IIC.controls.Player.Shoot.triggered)
        {
            currentState = GHStates.fire;

        }

        //sets the animation to the returned state
        AnimReturned(true);

        //makes sure to hook isnt parentet to anything
        transform.parent = null;

    }

    void IsFireing()
    {
        rb.isKinematic = false;
        rb.velocity = Camera.main.transform.forward * fireSpeed;

        //moves the current state to the next stage
        currentState = GHStates.travling;

        //sets the animation to the returned bool off
        AnimReturned(false);

        //activates the fire VFX
        PlayVFX(1);
    }
    
    void IsTraveling()
    {
        if ((transform.position - handPos.position).magnitude > hookMaxDistance)
        {
            ReturningMiddleStep();
        }

        //option to return hook
        if (IIC.controls.Player.Shoot.triggered)
        {
            AnimHooked(true);
            ReturningMiddleStep();
        }

        //ray colliding system
        
        //draws the gizmo
        Debug.DrawRay(transform.position, rb.velocity.normalized * maxRayDistance, Color.red, 10f);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, rb.velocity.normalized, out hit, maxRayDistance, layerMask))
        {
            
            globalHit = hit;
            if (hit.collider.tag == "NotHookable")
            {
                storedVelocity = rb.velocity;
                //Debug.Log("not hookable");
                currentState = GHStates.knockoff;
                

            }
            else
            {
                rb.isKinematic = true;
                transform.position = hit.point;

                var rot = Quaternion.LookRotation(-hit.normal);
                //Debug.Log(rot);

                //activates the hooked VFX
                //ActivateHookedVFX();
                PlayVFX(0);

                //activates the IsHooked animation bool
                AnimHooked(true);


                //transform.rotation = Quaternion.Euler(hit.normal);
                transform.rotation = rot;
                currentState = GHStates.hooked;
            }
        }
        

    }

    private bool isHookedEnter;
    void IsHooked()
    {
        if (isHookedEnter == false) //enter
        {
            isHookedEnter = true;

            //parent = global hit, makes it so that hook follow roation and movment to the obj its hooked to
            transform.parent = globalHit.collider.transform;
        }
        


        if (IIC.controls.Player.Shoot.triggered)
        {
            ReturningMiddleStep();
            isHookedEnter = false;
            transform.parent = null;
        }
    }

    private float retruningLerp;
    void ReturningMiddleStep()
    {
        //defines the distance between hook and player for returning calcs
        d = Vector3.Distance(handPos.position, transform.position);

        retruningLerp = 1f;

        currentState = GHStates.returning;

        // sets q equals the rotation so the Lerp in isReturning() work properly
        q = transform.rotation;
    }
    
    public float returnTime;

    public float rotLerpTime;
    private Quaternion q;
    void IsReturning()
    {
        //makes the hook kinematic so it dosent do wierd stuff
        rb.isKinematic = true;

        //sets the globalHit = null since it dosent have one anymore
        globalHit = new RaycastHit();

        // returning code

        //slowsy decreases d
        retruningLerp = retruningLerp - (1/returnTime) * Time.deltaTime;

        transform.position = handPos.position + 
            (transform.position - handPos.position).normalized * d * retruningLerp;

        // Lerp rotates the hook so it points towards the player

        var LerpQ = Quaternion.LookRotation((transform.position - handPos.position), Vector3.up);
        q = Quaternion.Lerp(q, LerpQ, rotLerpTime * Time.deltaTime);
        transform.rotation = q;

        if (retruningLerp < 0f)
        {
            currentState = GHStates.rest;
        }


        //Sets the Animation to the returning state
        AnimHooked(false);
    }

    private bool onEnter;
    [Tooltip("how fast the hook is luanced in the normal direction")]
    public float normalSpeed;
    [Tooltip("how fast the hook it rotated")]
    public float rotationSpeed;
    void KnockOff()
    {
        //On enter
        if (onEnter)
        {
            //playes the VFX 
            hookVFX.PlayKnockoffVFX(globalHit.point, Quaternion.LookRotation(globalHit.normal, Vector3.Cross(globalHit.normal,
                storedVelocity)), globalHit.collider.gameObject.transform);

            onEnter = false;
            var randomDirection = Random.onUnitSphere + globalHit.normal * normalSpeed;
            rb.velocity = randomDirection * 10f;
            rb.AddTorque(Random.insideUnitSphere * rotationSpeed, ForceMode.VelocityChange);

            //sets the animation to hooked
            AnimHooked(true);

            //activates the collider so it sits on the ground
            gameObject.GetComponent<BoxCollider>().enabled = transform;

            
        }

        if (IIC.controls.Player.Shoot.triggered)
        {
            ReturningMiddleStep();
            onEnter = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }


    }

    void ActivateHookedVFX()
    {
        visualEffect.Play();
    }

    void PlayVFX(int i)
    {
        hookVFX.PlayAnimation(i, globalHit.point, Quaternion.LookRotation(globalHit.normal));
    }

    //animation section
    public void AnimHooked(bool b)
    {
        animator.SetBool("IsHooked", b);
    }

    void AnimReturned(bool b)
    {
        animator.SetBool("Returned", b);
    }

}
