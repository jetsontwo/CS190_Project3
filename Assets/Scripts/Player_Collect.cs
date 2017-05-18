using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collect : MonoBehaviour {

    private GameObject object_held = null, touching;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && touching)
        {
            if (object_held == null)
                pickup();
            else
                setdown();
        }
    }

    void pickup()
    {
        object_held = touching;
        object_held.GetComponent<BoxCollider>().enabled = false;
        object_held.transform.parent = gameObject.transform;
        object_held.transform.localPosition = new Vector3(0.75f, 0, 0.75f);
        object_held.transform.localRotation = Quaternion.identity;
    }

    void setdown()
    {
        object_held.GetComponent<BoxCollider>().enabled = true;
        object_held.transform.parent = null;

        RaycastHit rh;
        Physics.Raycast(new Ray(transform.position, transform.forward), out rh, 1);

        print(rh.collider);
        object_held.transform.position = transform.position + transform.forward;

        //Can do a raycast here to see if the pedistal to put it down is there 
        //Else just drop it as it does now

        object_held = null;
        touching = null;

    }

	void OnTriggerEnter(Collider c)
    {
        if(!touching)
        {
            touching = c.gameObject;
            
        }
    }
    void OnTriggerExit(Collider c)
    {
        if (c.gameObject == touching)
            touching = null;
    }
}
