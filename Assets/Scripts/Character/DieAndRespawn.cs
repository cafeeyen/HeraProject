using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAndRespawn : MonoBehaviour { 
	public Vector3 SPAWN_POSITION = new Vector3(0, 0, 0);

	public HeraControl player;

    // Use this for initialization
    void Start () {
        player = GetComponent<HeraControl>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnDied()
	{
		
	}

	 public void movePlayer(){
		 player.transform.position = SPAWN_POSITION;
	 }
}
