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
        if(!stop_rotate)
        {
            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * rotate_speed, 0));

            if (-Input.GetAxis("Mouse Y") * rotate_speed > 50 && cam.transform.eulerAngles.x <= 50)
                cam.transform.Rotate(new Vector3(50, 0, 0));
            else if (-Input.GetAxis("Mouse Y") * rotate_speed < 310 && cam.transform.eulerAngles.x >= 310)
                cam.transform.Rotate(new Vector3(310, 0, 0));
            else
                cam.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * rotate_speed, 0, 0));
            
            print(cam.transform.eulerAngles);
            //if (cam.transform.eulerAngles.x > 60)
            //    cam.transform.eulerAngles = (new Vector3(60, 0, 0));
            //else if (cam.transform.eulerAngles.x < -40)
            //    cam.transform.eulerAngles = (new Vector3(-40, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
            stop_rotate = !stop_rotate;
        }
    }
}
