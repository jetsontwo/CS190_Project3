using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambient_Sounds : MonoBehaviour {

    public string[] soundEvent;

	// Use this for initialization
	void Start () {
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 4f));
            AkSoundEngine.PostEvent(soundEvent[Random.Range(0, soundEvent.Length)], gameObject);
        }
    }
}
