using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Track_Mouse : MonoBehaviour {

    public float rotate_speed;	
	// Update is called once per frame
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
	void Update () {
        transform.Rotate(new Vector3(0,Input.GetAxis("Mouse X") * rotate_speed, 0));

        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
	}
}
