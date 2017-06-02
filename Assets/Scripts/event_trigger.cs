using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class event_trigger : MonoBehaviour {

    public QuickCutsceneController mycutscene;

    private Camera myCamera;

    public GameObject myplayer;

    private Vector3 camera_old_position;
    private Quaternion camera_old_rotation;

    private Vector3 player_old_position;
    private Quaternion player_old_rotation;

    // Use this for initialization
    void Start () {
        myCamera = Camera.main;
        playThis();
    }
    /*
    //collider way:
    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            myplayer = other.gameObject;
            player_old_position = other.transform.position;
            camera_old_position = myCamera.transform.localPosition;
            player_old_rotation = other.transform.rotation;
            camera_old_rotation = myCamera.transform.localRotation;

            if (mycutscene.playingCutscene == false)
            {
                mycutscene.ActivateCutscene();
                Debug.Log("start camera!");
                //Start event:
                StartCoroutine("fish_event");

                StartCoroutine("Finish");
                myplayer.gameObject.GetComponent<Camera_Track_Mouse>().enabled = false;
                myplayer.gameObject.GetComponent<Movement>().enabled = false;

                
            }

        }
    }
    */
    IEnumerator Finish()
    {
        Debug.Log("Finishing!!");
        yield return new WaitForSeconds(14f);
        myplayer.gameObject.GetComponent<Camera_Track_Mouse>().enabled = true;
        myplayer.gameObject.GetComponent<Movement>().enabled = true;
        
        myCamera.transform.localPosition = camera_old_position;
        myCamera.transform.localRotation = camera_old_rotation;

        myplayer.transform.position = player_old_position;
        myplayer.transform.rotation = player_old_rotation;

        mycutscene.EndCutscene();
    }

    IEnumerator fish_event()
    {
        Debug.Log("flying!");
        yield return new WaitForSeconds(3f);
        flying_fish.Play();
    }

    void playThis()
    {
        player_old_position = myplayer.transform.position;
        camera_old_position = myCamera.transform.localPosition;
        player_old_rotation = myplayer.transform.rotation;
        camera_old_rotation = myCamera.transform.localRotation;

        if (mycutscene.playingCutscene == false)
        {
            mycutscene.ActivateCutscene();
            Debug.Log("start camera!");
            //Start event:
            try
            {
                StartCoroutine("fish_event");
            }
            catch
            {

            }

            StartCoroutine("Finish");
            myplayer.gameObject.GetComponent<Camera_Track_Mouse>().enabled = false;
            myplayer.gameObject.GetComponent<Movement>().enabled = false;

        }
    }
}
