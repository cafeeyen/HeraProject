using System.Collections.Generic;
using UnityEngine;

public class EnterBossChecker : MonoBehaviour
{
    public List<Collider> bossClose;

    private GameObject player;
    private GameObject boss;
    private MiaNoiAIController mianoi;

	void Start ()
    {
        player = GameObject.FindWithTag("Player");
        boss = GameObject.FindWithTag("Boss");
        mianoi = GameObject.FindWithTag("Boss").GetComponent<MiaNoiAIController>();
        setWallCollider(false);
    }

	void Update ()
    {
        if(boss == null)
        {
            setWallCollider(false);
            Destroy(gameObject);
        }
		else if(!boss.activeSelf)
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

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            mianoi.enableBossMoving();
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
