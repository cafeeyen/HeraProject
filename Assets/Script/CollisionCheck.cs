using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{

    private DieAndRespawn dieAndRespawn;
    private PlayerInventory pInventory;


    // Use this for initialization
    void Start ()
    {
		dieAndRespawn = gameObject.GetComponent<DieAndRespawn>();
        pInventory = gameObject.GetComponent<PlayerInventory>();
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;
        else if (body.gameObject.tag == "Water")
        {
            Debug.Log("Collide");
            dieAndRespawn.OnDied();
        }

        //Player pickup item here
        else if (body.gameObject.tag == "Item01")
        {
            Debug.Log("Got Item01");
            pInventory.addItem(body.gameObject.GetComponent<Item01>());
            Destroy(body.gameObject);
        }
    }
}
