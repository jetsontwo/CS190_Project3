using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private Rigidbody rb;
    private float vert, horiz;
    public float max_vel, speed, decelleration, jump_power, ground_search_dist;
    private bool on_Ground = true;
    public static bool allow_move = true;

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
        if (rb.velocity.y == 0.00 && !on_Ground)
        {
            //Debug.Log("standing still!");
            on_Ground = true;
        }
        else if(!on_Ground)
        {
            Debug.DrawRay(transform.position, -transform.up * ground_search_dist);
            if (Physics.Raycast(transform.position, -transform.up * ground_search_dist, LayerMask.NameToLayer("Ground")) && rb.velocity.y == 0.00000)
            {
                Debug.Log("Change!");
                on_Ground = true;
            }
            else
            {
                on_Ground = false;
            }
        }



        if (allow_move)  //if in the air, just fly, cannot move
        {
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
            else if (vert == 0 && horiz == 0 && rb.velocity.magnitude != 0)
                rb.velocity -= new Vector3(rb.velocity.x / 2, 0, rb.velocity.z / 2);

        }

        if(on_Ground && Input.GetKeyDown(KeyCode.Space))
        {
            allow_move = false;
            //todo: jump sound here
            AkSoundEngine.PostEvent("Player_jumping", gameObject);
            on_Ground = false;
            rb.AddForce(new Vector3(0, jump_power, 0));
            //Debug.Log("in the air!");
        }
        

        MoveVector = new Vector3(horiz, 0, vert);

        if (IsWalking && on_Ground)
        {
            if (WalkTimer == 0)
            {
                if (Position != LastPosition)
                {
                    //Debug.Log(playerWalkingState);
                    //Debug.Log(on_Ground);
                    if (on_Ground)
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
                    }
                    WalkTimer += Time.deltaTime;
                }
            }
            else if (WalkTimer >= FootStepInterval)
            {
                WalkTimer = 0;
            }
            else
            {
                WalkTimer += Time.deltaTime;
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
        //Debug.Log("enter!");
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
            speedfactor = 1f;
            speed = 20;
        }
        if (other.gameObject.tag == "dirt")
        {
            playerWalkingState = walkingState.dirt;
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

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("stay!");
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

            speedfactor = 1f;
            speed = 20;
        }
        if (other.gameObject.tag == "dirt")
        {
            playerWalkingState = walkingState.dirt;

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
        //Debug.Log("exit!");
        if (other.gameObject.tag == "gravel")
        {
            playerWalkingState = walkingState.Ground;
      
        }
        if (other.gameObject.tag == "water")
        {
            playerWalkingState = walkingState.Ground;
            speedfactor = 1.5f;
            speed = 50;
      
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

    IEnumerator delay()
    {
        //yield return new WaitForSeconds(0.1f);
        AkSoundEngine.PostEvent("Player_landing", gameObject);
        yield return new WaitForSeconds(0f);
        //Debug.Log("canmovenow");
        allow_move = true;
        on_Ground = true;
        
    }

    //need this to collide with terrain collider
    void OnCollisionEnter(Collision collision)
    {
        //foreach (ContactPoint contact in collision.contacts)
        //{
        //    Debug.DrawRay(contact.point, contact.normal, Color.white);
        //}

        //if (collision.relativeVelocity.magnitude > 1)
        //{
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Terrain" || collision.gameObject.tag == "Post")
        {
            if (!allow_move)
            {
                    
                StartCoroutine(delay());
                    
            }
        }
        
    }

}
