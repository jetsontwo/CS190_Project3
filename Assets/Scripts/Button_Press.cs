using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Press : MonoBehaviour {

	public void onClick()
    {
        AkSoundEngine.PostEvent("ButtonPress",gameObject);
        AkSoundEngine.PostEvent("stopAll", gameObject);
    }
}
