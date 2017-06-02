using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_Sounds : MonoBehaviour {

    public GameObject[] list_of_objects;

    public IEnumerator activate(float time_between = 0)
    {
        foreach (GameObject item in list_of_objects)
        {
            item.SetActive(true);
            yield return new WaitForSeconds(time_between);
        }
        this.GetComponent<event_trigger>().enabled = true;



    }
}
