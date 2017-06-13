using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Track_Mouse : MonoBehaviour {

    public float rotate_speed;
    private GameObject cam;
    private bool stop_rotate;
	// Update is called once per frame
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main.gameObject;
    }
	void Update () {
            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * rotate_speed, 0));

            //print(cam.transform.eulerAngles);
            //float move_y = -Input.GetAxis("Mouse Y") * rotate_speed;
            //if (cam.transform.rotation.eulerAngles.z == 180)
            //{
            //    if (cam.transform.rotation.eulerAngles.x > 80 && cam.transform.rotation.eulerAngles.x < 100)
            //    {
            //        if (move_y < 0)
            //            move_y = 0;
            //    }
            //    else
            //    {
            //        if (move_y > 0)
            //            move_y = 0;
            //    }
            //}
            cam.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * rotate_speed, 0, 0));

    }
}
