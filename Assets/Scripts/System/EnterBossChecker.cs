using System.Collections.Generic;
using UnityEngine;

public class EnterBossChecker : MonoBehaviour
{
    public List<Collider> bossClose;

    private GameObject player;
    private GameObject boss;

	void Start ()
    {
        player = GameObject.FindWithTag("Player");
        boss = GameObject.FindWithTag("Boss");
        setWallCollider(false);
    }

	void Update ()
    {
		if(!boss.activeSelf)
        {
            setWallCollider(false);
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            setWallCollider(true);
        }
    }

    private void setWallCollider(bool state)
    {
        foreach (Collider c in bossClose)
        {
            c.enabled = state;
        }
    }
}
