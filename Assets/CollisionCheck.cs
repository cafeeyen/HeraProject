using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour {

    public DieAndRespawn dieAndRespawn = new DieAndRespawn();


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

	void OnCollisionEnter (Collision col)
    {
        Debug.Log("------Collide");
        if (col.gameObject.tag == "Water")
        {
            Debug.Log("Collide");
            Destroy(col.gameObject);
            dieAndRespawn.OnDied();
        }
    }

    void OnCollisionExit (Collision col)
    {
        Debug.Log("Collide Out -------");
    }

}
