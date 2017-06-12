using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class summon_flying_fish : MonoBehaviour {

    public GameObject myprefab;
	// Use this for initialization
	void Start () {
        var myfish = Instantiate(myprefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        myfish.transform.parent = gameObject.transform;

        var myfish2 = Instantiate(myprefab, new Vector3((transform.position.x-2), transform.position.y, (transform.position.z+2)), Quaternion.identity);
        myfish2.transform.parent = gameObject.transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
