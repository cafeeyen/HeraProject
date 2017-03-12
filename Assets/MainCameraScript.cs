using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour {

	public GameObject player;
    private Vector3 offset;

    
    void Start () 
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 24, player.transform.position.z - 25);
        offset = transform.position - player.transform.position;
    }
    
    void LateUpdate () 
    {
        transform.position = player.transform.position + offset;
    }
}
