using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Anim : MonoBehaviour {

    public float low, high, bob_dist;

    void OnEnable()
    {
        StartCoroutine(Bob(transform.position.y, bob_dist));
    }
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, Random.Range(low, high),0));
	}


    private IEnumerator Bob(float init_y, float bob_dist)
    {
        while(true)
        {
            while (transform.position.y < init_y + bob_dist)
            {
                transform.Translate(new Vector3(0, 0.007f, 0));
                yield return new WaitForSeconds(0.01f);
            }
            while (transform.position.y > init_y)
            {
                transform.Translate(new Vector3(0, -0.007f, 0));
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
