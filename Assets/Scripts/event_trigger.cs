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
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            myplayer = other.gameObject;
            player_old_position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y+0.8f, this.gameObject.transform.position.z);
            camera_old_position = new Vector3(player_old_position[0], player_old_position[1]+1.25f, player_old_position[2]);
            player_old_rotation = new Quaternion(myplayer.transform.rotation.x, myplayer.transform.rotation.y, myplayer.transform.rotation.z, myplayer.transform.rotation.w);
            camera_old_rotation = new Quaternion(myCamera.transform.rotation.x, myCamera.transform.rotation.y, myCamera.transform.rotation.z, myCamera.transform.rotation.w);

            if (mycutscene.playingCutscene == false)
            {
                mycutscene.ActivateCutscene();
                Debug.Log("previous rotation: " + camera_old_position[0].ToString() + " " + camera_old_position[1].ToString() + " " + camera_old_position[2].ToString());
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
        
        myCamera.transform.position = camera_old_position;
        myCamera.transform.rotation = camera_old_rotation;

        myplayer.transform.position = player_old_position;
        myplayer.transform.rotation = player_old_rotation;

        Debug.Log("Done rotation: " + myCamera.transform.position.x.ToString() + " " + myCamera.transform.position.y.ToString() + " " + myCamera.transform.position.z.ToString());
        mycutscene.EndCutscene();
    }
}
