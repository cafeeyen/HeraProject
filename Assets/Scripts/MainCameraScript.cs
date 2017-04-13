using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour {

	public GameObject player;
    private Vector3 offset;
    private float X_CAMERA = 0, Y_CAMERA = 60, Z_CAMERA = -60;

    
    void Start () 
    {
        transform.position = new Vector3(player.transform.position.x + X_CAMERA, player.transform.position.y + Y_CAMERA, player.transform.position.z + Z_CAMERA);
        offset = transform.position - player.transform.position;
        //player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void LateUpdate () 
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position + offset;
    }
}
