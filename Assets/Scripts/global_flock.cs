using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class global_flock : MonoBehaviour {

    public GameObject myprefab;
    public GameObject goalprefab;

    public Transform original_position;

    public static Transform Position;

    public static int world_size_x = 10;
    public static int world_size_y = 2; 
    public static int world_size_z = 10;

    public static int num_of_objects = 20;

    public static GameObject[] mygroup = new GameObject[num_of_objects];

    public static Vector3 final_position = Vector3.zero;
	
    void awake()
    {
        Position = transform;
    }

    // Use this for initialization
	void Start () {
        Position = transform;

        for (int i = 0; i < num_of_objects; i++)
        {
            Vector3 position = new Vector3(original_position.position.x + Random.Range(-world_size_x, world_size_x),
                                    original_position.position.y + Random.Range(-world_size_y, world_size_y),
                                    original_position.position.z + Random.Range(-world_size_z, world_size_z));
            mygroup[i] = (GameObject)Instantiate(myprefab, position, Quaternion.identity);
        }
        	
	}
	
	// Update is called once per frame
	void Update () {
		if(Random.Range(0,1000) < 50)
        {
            final_position = new Vector3(original_position.position.x + Random.Range(-world_size_x, world_size_x),
                                    original_position.position.y + Random.Range(-world_size_y, world_size_y),
                                    original_position.position.z + Random.Range(-world_size_z, world_size_z));
            //goalprefab.transform.position = final_position;
        }
	}
}
