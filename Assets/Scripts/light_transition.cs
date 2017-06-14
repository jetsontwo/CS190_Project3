using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_transition : MonoBehaviour {

    public static light_transition instance;
    // Use this for initialization
    void Awake()
    {
        instance = this;
    }
    void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
