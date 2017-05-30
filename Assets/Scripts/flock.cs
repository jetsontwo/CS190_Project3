using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flock : MonoBehaviour {

    public float speed = 3.0f;
    public float rotation_speed = 4.0f;
    Vector3 average_Heading;
    Vector3 average_Position;
    public float neighbor_distance = 1.0f;

    bool turning = false;

	// Use this for initialization
	void Start () {
        speed = Random.Range(0.5f, 1);	
	}
	
	// Update is called once per frame
	void Update () {

        if(Vector3.Distance(transform.position,global_flock.Position.position)>= global_flock.world_size_x)
        {
            turning = true;
        }
        else
        {
            turning = false;

        }

        if(turning)
        {
            Vector3 direction = global_flock.Position.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotation_speed * Time.deltaTime);
            speed = Random.Range(0.5f, 1);
        }
        else
        {
            if (Random.Range(0, 5) < 1)
            {
                flocking_rules();
            }
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
	}
    void flocking_rules()
    {
        GameObject[] group_pos;
        group_pos = global_flock.mygroup;

        Vector3 vcentre = global_flock.Position.position;
        Vector3 vavoid = Vector3.zero;

        float group_speed = 0.1f;

        Vector3 final_position = global_flock.final_position;
        float distance;
        int group_size = 0;

        foreach (GameObject mygo in group_pos)
        {
            if(mygo != this.gameObject)
            {
                distance = Vector3.Distance(mygo.transform.position, this.transform.position);
                if(distance <= neighbor_distance)
                {
                    vcentre += mygo.transform.position;
                    group_size++;

                    if(distance < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - mygo.transform.position);
                    }

                    flock another_object = mygo.GetComponent<flock>();
                    group_speed = group_speed + another_object.speed;

                }
            }
        }

        if (group_size > 0)
        {
            vcentre = vcentre / group_size + (final_position - this.transform.position);
            speed = group_speed / group_size;

            Vector3 direction = (vcentre + vavoid) - transform.position;

            if(direction != global_flock.Position.position)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotation_speed * Time.deltaTime);
            }
        }

    }
}
