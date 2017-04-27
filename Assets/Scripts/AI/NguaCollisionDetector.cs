using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NguaCollisionDetector : MonoBehaviour {

	public static bool isColliding = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // || other.gameObject.CompareTag("Wall")
		{
			isColliding = true;
		}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
		{
			isColliding = false;
		}
    }
}
