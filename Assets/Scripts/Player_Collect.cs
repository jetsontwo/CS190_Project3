using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collect : MonoBehaviour {

    private GameObject object_held = null, touching;
    public float drop_dist;

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
        if (touching.tag != "Collect")
            return;
        AkSoundEngine.PostEvent("pickup", gameObject);
        object_held = touching;
        object_held.GetComponent<BoxCollider>().enabled = false;
        object_held.GetComponent<Box_Anim>().enabled = false;
        object_held.GetComponent<Box_Anim>().StopAllCoroutines();
        object_held.GetComponent<ParticleSystem>().Stop();
        object_held.transform.parent = gameObject.transform;
        object_held.transform.localPosition = new Vector3(0.75f, 0, 0.75f);
        object_held.transform.localRotation = Quaternion.identity;
    }

    void setdown()
    {
        object_held.GetComponent<BoxCollider>().enabled = true;
        object_held.transform.parent = null;

        RaycastHit rh;
        Physics.Raycast(new Ray(transform.position, transform.forward), out rh, 1.5f);

        if (rh.collider)
        {
            if(rh.collider.gameObject.tag == "Post")
            {
                //Checks to make sure the item can be placed there
                if (rh.collider.gameObject.GetComponent<Post_Controller>().post_item(object_held))
                {
                    object_held = null;
                    touching = null;
                }
                else
                    pickup();
            }
        }
        else
        {
            object_held.transform.position = new Vector3(transform.position.x + transform.forward.x, transform.position.y - drop_dist, transform.position.z + transform.forward.z);
            object_held.GetComponent<Box_Anim>().enabled = true;
            object_held.GetComponent<ParticleSystem>().Play();

            object_held = null;
            touching = null;
        }

        //Can do a raycast here to see if the pedistal to put it down is there 
        //Else just drop it as it does now



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
