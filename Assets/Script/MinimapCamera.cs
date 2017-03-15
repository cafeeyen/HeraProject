using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour {

	private float MINIMAP_YZOOM = 150;
	private float MINIMAP_ZFRONT = 1;
	public Transform playerController;
    private Vector3 offset;

    
    void Start () 
    {
        transform.position = new Vector3(playerController.transform.position.x, playerController.transform.position.y + MINIMAP_YZOOM,
		 playerController.transform.position.z + MINIMAP_ZFRONT);
        offset = transform.position - playerController.transform.position;
    }
    
    void LateUpdate () 
    {
        transform.position = playerController.transform.position + offset;
		//Debug.Log(playerController.transform.position);
    }
}
