using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpyCollisionDetector : MonoBehaviour {

	private HarpyAIController controller;

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
