using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private Rigidbody rb;
    private float vert, horiz;
    public float max_vel, speed, decelleration;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        vert = Input.GetAxisRaw("Vertical");
        horiz = Input.GetAxisRaw("Horizontal");
        if (vert != 0 && rb.velocity.magnitude < max_vel)
            rb.velocity += new Vector3(0, 0, vert);
        else if (vert == 0)
            rb.velocity -= new Vector3(0, 0, rb.velocity.z / decelleration);
        if (horiz != 0 && rb.velocity.magnitude < max_vel)
            rb.velocity += new Vector3(horiz, 0, 0);
        else if (horiz == 0)
            rb.velocity -= new Vector3(rb.velocity.x / decelleration, 0, 0);

        if(vert != 0 || horiz != 0)
        {
            rb.velocity.Normalize();
            rb.velocity *= Time.deltaTime * speed;
        }

    }
}
