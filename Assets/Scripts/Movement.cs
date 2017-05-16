using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private Rigidbody rb;
    private float vert, horiz;
    public float max_vel, speed, decelleration;

    private Vector3 MoveVector = Vector3.zero;
    public bool IsWalking
    {
        get {
            if (Vector3.Distance(MoveVector,Vector3.zero) != 0)
            {
                return true;
            }
            return false; }
    }

    private Vector3 Position
    {
        get { return rb.position; }
        set { rb.position = value; }
    }
    private Vector3 LastPosition;

    private float WalkTimer = 0.0f;
    private float speedfactor = 1.5f;
    private float FootStepInterval
    {
        get { return (speed/(speed+5)) / speedfactor; } //second speed can be adjusted for running
    }

    //Walking State:
    public enum walkingState { Ground,concrete,gravel,water,dirt };
    public walkingState playerWalkingState = walkingState.Ground;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        vert = Input.GetAxisRaw("Vertical");
        horiz = Input.GetAxisRaw("Horizontal");

        
        if (vert != 0 && rb.velocity.magnitude < max_vel)
            rb.velocity += transform.forward * vert;
        if (horiz != 0 && rb.velocity.magnitude < max_vel)
            rb.velocity += transform.right * horiz;


        if (vert != 0 || horiz != 0)
        {
            rb.velocity.Normalize();
            rb.velocity *= Time.deltaTime * speed;
        }
        

        MoveVector = new Vector3(horiz, 0, vert);
        //rb.MovePosition(rb.position + MoveVector * (speed/5f) * Time.fixedDeltaTime);
        //Debug.Log(playerWalkingState);

        if (IsWalking)
        {
            if (WalkTimer == 0)
            {
                if (Position != LastPosition)
                {
                    switch (playerWalkingState)
                    {
                        case walkingState.concrete:
                            AkSoundEngine.PostEvent("PlayerWalk_concrete", gameObject);     
                            break;
                        case walkingState.gravel:
                            AkSoundEngine.PostEvent("PlayerWalk_gravel", gameObject);     
                            break;
                        case walkingState.water:
                            AkSoundEngine.PostEvent("PlayerWalk_water", gameObject);     
                            break;
                        case walkingState.dirt:
                            AkSoundEngine.PostEvent("PlayerWalk_dirt", gameObject);     
                            break;
                        case walkingState.Ground:
                            AkSoundEngine.PostEvent("PlayerWalk_dirt", gameObject);
                            break;
                        default:
                            AkSoundEngine.PostEvent("PlayerWalk_dirt", gameObject);
                            break;
                    }

                    WalkTimer += Time.fixedDeltaTime;
                }
            }
            else if (WalkTimer >= FootStepInterval)
            {
                WalkTimer = 0;
            }
            else
            {
                WalkTimer += Time.fixedDeltaTime;
            }
            LastPosition = Position;
        }
        else
        {
            WalkTimer = 0;
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "concrete")
        {
            playerWalkingState = walkingState.concrete;
        }
        if (other.gameObject.tag == "gravel")
        {
            playerWalkingState = walkingState.gravel;
        }
        if (other.gameObject.tag == "water")
        {
            playerWalkingState = walkingState.water;
        }
        if (other.gameObject.tag == "dirt")
        {
            playerWalkingState = walkingState.dirt;
        }
        if (other.gameObject.tag == "Ground")
        {
            playerWalkingState = walkingState.Ground;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "gravel")
        {
            playerWalkingState = walkingState.Ground;
        }
        if (other.gameObject.tag == "water")
        {
            playerWalkingState = walkingState.Ground;
        }
        if (other.gameObject.tag == "dirt")
        {
            playerWalkingState = walkingState.Ground;
        }
        if (other.gameObject.tag == "Ground")
        {
            playerWalkingState = walkingState.concrete;
        }

    }

}
