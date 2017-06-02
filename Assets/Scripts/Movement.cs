using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private Rigidbody rb;
    private float vert, horiz;
    public float max_vel, speed, decelleration, jump_power, ground_search_dist;
    private bool on_Ground = true;

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
        //Ground detection
        if (rb.velocity.y == 0 && !on_Ground)
            on_Ground = true;
        else
        {
            Debug.DrawRay(transform.position, -transform.up * ground_search_dist);
            if(Physics.Raycast(transform.position, -transform.up * ground_search_dist, LayerMask.NameToLayer("Ground")) && rb.velocity.y == 0)
            {
                on_Ground = true;
            }
            else
                on_Ground = false;

        }




        vert = Input.GetAxisRaw("Vertical");
        horiz = Input.GetAxisRaw("Horizontal");


        if (horiz != 0 && vert != 0 && rb.velocity.magnitude < max_vel)
        {
            rb.velocity += (transform.forward * vert) * Time.deltaTime * speed;
            rb.velocity += transform.right * horiz * Time.deltaTime * speed;
        }
        else if (vert != 0 && rb.velocity.magnitude < max_vel)
            rb.velocity += transform.forward * vert * Time.deltaTime * speed;
        else if (horiz != 0 && rb.velocity.magnitude < max_vel)
            rb.velocity += transform.right * horiz * Time.deltaTime * speed;





        if(on_Ground && Input.GetKeyDown(KeyCode.Space))
        {
            on_Ground = false;
            rb.AddForce(new Vector3(0, jump_power, 0));
        }
        

        MoveVector = new Vector3(horiz, 0, vert);
        //rb.MovePosition(rb.position + MoveVector * (speed/5f) * Time.fixedDeltaTime);
        Debug.Log(playerWalkingState);

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
                            AkSoundEngine.PostEvent("PlayerWalk_water_close", gameObject);     
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

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "concrete")
        {
            playerWalkingState = walkingState.concrete;
            on_Ground = true;
        }
        if (other.gameObject.tag == "gravel")
        {
            playerWalkingState = walkingState.gravel;
            on_Ground = true;
        }
        if (other.gameObject.tag == "water")
        {
            playerWalkingState = walkingState.water;
            on_Ground = true;
            speedfactor = 1f;
            speed = 20;
        }
        if (other.gameObject.tag == "dirt")
        {
            playerWalkingState = walkingState.dirt;
            on_Ground = true;

            speedfactor = 1.5f;
            speed = 50;
        }
        if (other.gameObject.tag == "Ground")
        {
            playerWalkingState = walkingState.Ground;
            speedfactor = 1.5f;
            speed = 50;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "gravel")
        {
            playerWalkingState = walkingState.Ground;
            on_Ground = false;
        }
        if (other.gameObject.tag == "water")
        {
            playerWalkingState = walkingState.Ground;
            on_Ground = false;
            speedfactor = 1.5f;
            speed = 50;
        }
        if (other.gameObject.tag == "dirt")
        {
            playerWalkingState = walkingState.Ground;
            on_Ground = false;
        }
        if (other.gameObject.tag == "Ground")
        {
            playerWalkingState = walkingState.concrete;
            on_Ground = false;
        }

    }

}
