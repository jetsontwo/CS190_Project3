using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class last_event : MonoBehaviour {

    [HideInInspector]
    public static int collectable_count = 0;
    public static bool status = false;
	// Use this for initialization
	void Start () {
		
	}

    IEnumerator start_last_event()
    {
        yield return new WaitForSeconds(20f);
        Debug.Log("Last Event start!");
        status = true;
        Movement.allow_move = false;
        this.GetComponent<event_trigger>().enabled = true;

        yield return new WaitForSeconds(24f);
        //end game scene here:
        SceneManager.LoadScene(2);
    }

    // Update is called once per frame
    void Update () {
	    if(collectable_count == 5)
        {
            Debug.Log("Last Event loading...");
            collectable_count = 0;
            StartCoroutine(start_last_event());
        }
    }
}
