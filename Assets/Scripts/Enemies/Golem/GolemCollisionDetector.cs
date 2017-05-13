using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemCollisionDetector : MonoBehaviour {

	private GolemAIController controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
		{

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
		{

        }
    }
}
