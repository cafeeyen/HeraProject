using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour {

	public GameObject player;
    private Vector3 offset;
    private float X_CAMERA = 0, Y_CAMERA = 40, Z_CAMERA = -40;

    
    void Start () 
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + Y_CAMERA, player.transform.position.z + Z_CAMERA);
        offset = transform.position - player.transform.position;
    }
    
    void LateUpdate () 
    {
        transform.position = player.transform.position + offset;
    }
}
