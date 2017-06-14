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
        yield return new WaitForSeconds(20.5f);
        light_transition.instance.GetComponent<Animator>().Play("light");
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Last Event start!");
        status = true;
        Movement.allow_move = false;
        AkSoundEngine.PostEvent("ending_music", gameObject);

        this.GetComponent<event_trigger>().enabled = true;

        yield return new WaitForSeconds(25f);
        Debug.Log("Last Event end!");
        //end game scene here:
        Cursor.lockState = CursorLockMode.None;

        AkSoundEngine.PostEvent("Stop_All_ending", gameObject);

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
