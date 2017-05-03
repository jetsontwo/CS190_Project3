using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collect : MonoBehaviour {

    private GameObject object_held;

	void OnTriggerEnter(Collider c)
    {
        if(!object_held)
        {
            object_held = c.gameObject;
            c.GetComponent<BoxCollider>().enabled = false;
            c.transform.parent = gameObject.transform;
            c.transform.localPosition = new Vector3(0.75f, 0, 0.75f);
            c.transform.localRotation = Quaternion.identity;
        }
    }
}
