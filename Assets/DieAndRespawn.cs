using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAndRespawn : MonoBehaviour { 

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < -10){
			OnDied();
		}
	}

	 public void OnDied(){
		 transform.position = Vector3.zero;
	 }
}
