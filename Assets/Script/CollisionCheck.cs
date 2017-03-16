using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour {

    private DieAndRespawn dieAndRespawn;
    private PlayerInventory pInventory;


    // Use this for initialization
    void Start () {
		dieAndRespawn = gameObject.GetComponent<DieAndRespawn>();
        pInventory = gameObject.GetComponent<PlayerInventory>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

	void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.tag == "Water")
        {
            Debug.Log("Collide");
            //Destroy(col.gameObject);
            dieAndRespawn.OnDied();
        }
        //player pick item here
        if (col.gameObject.tag == "Item01")
        {
            Debug.Log("Got Item01");
            pInventory.addItem(col.gameObject.GetComponent<Item01>());
        }
    }

    void OnCollisionExit (Collision col){}

}
