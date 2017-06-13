﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Post_Controller : MonoBehaviour {


    private GameObject item_held;
    public float spin_low, spin_high;
	// Update is called once per frame
	void Update () {
		if(item_held)
        {
            item_held.transform.Rotate(new Vector3(Random.Range(spin_low, spin_high), Random.Range(spin_low, spin_high), Random.Range(spin_low, spin_high)));
        }
	}

    public bool post_item(GameObject item)
    {
        if (item_held != null)
            return false;
        item_held = item;
        item_held.transform.parent = transform;
        item_held.transform.localPosition = new Vector3(0, 3f, 0);

        //stop the hint sound:
        //Debug.Log("placed!");
        AkSoundEngine.PostEvent(item.GetComponent<Ambient_Sounds>().stopthis_sound, item.gameObject);
        last_event.collectable_count += 1;

        //start cutscene:
        light_transition.instance.GetComponent<Animator>().Play("transition");

        StartCoroutine(item_held.GetComponent<Activate_Sounds>().activate(1f));
        return true;
    }
}
