using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private DieAndRespawn dieAndRespawn;
    private CharacterControl cc;
    private ParticleController par;

    // Use this for initialization
    void Start ()
    {
		dieAndRespawn = gameObject.GetComponent<DieAndRespawn>();
        cc = gameObject.GetComponent<CharacterControl>();
        par = gameObject.GetComponent<ParticleController>();
        
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
            PlayerInventory.inventory.addItem(new Item01());
            Destroy(body.gameObject);
            par.playParticle(new Color(1, 0, 0, 1));
        }
        else if (body.gameObject.tag == "Helmet01")
        {
            Debug.Log("Got Helmet01");
            PlayerInventory.inventory.addItem(new Helmet01());
            Destroy(body.gameObject);
            par.playParticle(new Color(1, 1, 0, 1));
        }
        else if (body.gameObject.tag == "Robe01")
        {
            Debug.Log("Got Robe01");
            PlayerInventory.inventory.addItem(new Robe01());
            Destroy(body.gameObject);
            par.playParticle(new Color(0, 1, 1, 1));
        }
        else if (body.gameObject.tag == "Pohpae01")
        {
            Debug.Log("Got Pohpae01");
            PlayerInventory.inventory.addItem(new Pohpae01());
            Destroy(body.gameObject);
            par.playParticle(new Color(1, 0.92f, 0.016f, 1));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("WalkableWater"))
            cc.isWater(true);
        if (other.gameObject.CompareTag("SavePoint"))
            GameData.data.posx = cc.gameObject.transform.position.x;
            GameData.data.posy = cc.gameObject.transform.position.y;
            GameData.data.posz = cc.gameObject.transform.position.z;
            //GameData.data.inventory = PlayerInventory.inventory.pIList;
            SaveLoad.Save();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("WalkableWater"))
            cc.isWater(false);
    }
}
