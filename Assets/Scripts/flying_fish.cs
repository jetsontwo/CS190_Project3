using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flying_fish : MonoBehaviour {

    public static flying_fish instance;

    void Awake()
    {
        instance = this;
    }
    
    void OnEnable()
    {
        instance.StartCoroutine("fish_event");
    }

    IEnumerator fish_event()
    {
        Debug.Log("flying!!!");
        yield return new WaitForSeconds(Random.Range(0.5f, 3f));
        instance.GetComponent<Animator>().Play("flying_fish");
    }

    // Use this for initialization
    void Start () {
        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void Play()
    {
        instance.StartCoroutine("fish_event");
    }
}
