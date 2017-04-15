using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAndRespawn : MonoBehaviour { 
	public Vector3 SPAWN_POSITION = new Vector3(0, 1, -628);

	public CharacterController characterController;

    // Use this for initialization
    void Start () {
        characterController = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
		if(characterController.transform.position.y < -10){
			OnDied();
		}
	}

	 public void OnDied(){
		 characterController.transform.position = SPAWN_POSITION;
	 }
}
