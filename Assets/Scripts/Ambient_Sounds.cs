﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambient_Sounds : MonoBehaviour {

    public string[] soundEvent;
    
    public string stopthis_sound;

	// Use this for initialization
	void Start () {
        StartCoroutine(StartDelay());

    }

    IEnumerator StartDelay()
    {

        if (gameObject.tag == "Collect")
        {
            yield return new WaitForSeconds(Random.Range(2f, 4f));
            AkSoundEngine.PostEvent(soundEvent[Random.Range(0, soundEvent.Length)], gameObject);
            
        }
        else
        {     
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(0f, 1f));
                AkSoundEngine.PostEvent(soundEvent[Random.Range(0, soundEvent.Length)], gameObject);
            }
        }
    }
}
