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
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            myplayer = other.gameObject;
            player_old_position = other.transform.position;
            camera_old_position = myCamera.transform.localPosition;
            print(camera_old_position);
            player_old_rotation = other.transform.rotation;
            camera_old_rotation = myCamera.transform.localRotation;

            if (mycutscene.playingCutscene == false)
            {
                mycutscene.ActivateCutscene();
                StartCoroutine("Finish");
                myplayer.gameObject.GetComponent<Camera_Track_Mouse>().enabled = false;
                myplayer.gameObject.GetComponent<Movement>().enabled = false;
            }

        }
    }
    IEnumerator Finish()
    {
        yield return new WaitForSeconds(10f);
        myplayer.gameObject.GetComponent<Camera_Track_Mouse>().enabled = true;
        myplayer.gameObject.GetComponent<Movement>().enabled = true;
        
        myCamera.transform.localPosition = camera_old_position;
        myCamera.transform.localRotation = camera_old_rotation;

        myplayer.transform.position = player_old_position;
        myplayer.transform.rotation = player_old_rotation;

        Debug.Log("Done rotation: " + myCamera.transform.position.x.ToString() + " " + myCamera.transform.position.y.ToString() + " " + myCamera.transform.position.z.ToString());
        mycutscene.EndCutscene();
    }
}
