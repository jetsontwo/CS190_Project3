using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collect : MonoBehaviour {

    private GameObject object_held;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            pickup();
    }

    void pickup()
    {
        object_held.GetComponent<BoxCollider>().enabled = false;
        object_held.transform.parent = gameObject.transform;
        object_held.transform.localPosition = new Vector3(0.75f, 0, 0.75f);
        object_held.transform.localRotation = Quaternion.identity;
    }
	void OnTriggerEnter(Collider c)
    {
        if(!object_held)
        {
            object_held = c.gameObject;
            
        }
    }
    void OnTriggerExit(Collider c)
    {
        if (c.gameObject == object_held)
            object_held = null;
    }
}
