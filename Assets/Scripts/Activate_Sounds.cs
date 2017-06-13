using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_Sounds : MonoBehaviour {

    public GameObject[] list_of_objects;
    public float time_between = 0f;

    public IEnumerator activate(float time_between_var = 0f)
    {
        if(time_between_var >= time_between)
        {
            time_between = time_between_var;
        }

        yield return new WaitForSeconds(time_between);
        this.GetComponent<event_trigger>().enabled = true;

        foreach (GameObject item in list_of_objects)
        {
            item.SetActive(true);
            yield return new WaitForSeconds(time_between);
        }



    }
}
