using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambient_Sounds : MonoBehaviour {

    public string soundEvent;

	// Use this for initialization
	void Start () {
        StartCoroutine(StartDelay());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(1);
        AkSoundEngine.PostEvent(soundEvent, gameObject);
    }
}
