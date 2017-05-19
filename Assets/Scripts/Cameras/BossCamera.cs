using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCamera : MonoBehaviour {

	private GameObject player;
	private GameObject boss;
    private Vector3 offset;
	private float startMoveTime, camMoveSpeed = 0.2f;
    private float X_CAMERA = 0, Y_CAMERA = 30, Z_CAMERA = -40;

    
    void Start () 
    {
		player = GameObject.FindWithTag("Player");
        boss = GameObject.FindWithTag("Boss");
        transform.position = new Vector3(boss.transform.position.x, boss.transform.position.y, boss.transform.position.z + -5);
        offset = transform.position - player.transform.position;
		startMoveTime = Time.time;
        //player = GameObject.FindGameObjectWithTag("Player");
    }

	void Update () 
	{

	}
    
    void LateUpdate () 
    {
		if(Time.time < (startMoveTime + 5))
		{
			transform.position = Vector3.MoveTowards(transform.position,  
				new Vector3(player.transform.position.x + X_CAMERA, 
				player.transform.position.y + Y_CAMERA, player.transform.position.z + Z_CAMERA), camMoveSpeed);
			camMoveSpeed *= 1.01f;
		}
		else
		{
			transform.position = new Vector3(player.transform.position.x + X_CAMERA, player.transform.position.y + Y_CAMERA, player.transform.position.z + Z_CAMERA);
		}
        
    }
}
