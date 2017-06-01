using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambient_Sounds : MonoBehaviour {

    public string soundEvent;

	// Use this for initialization
	void Start () {
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 3f));
        AkSoundEngine.PostEvent(soundEvent, gameObject);
    }
}
