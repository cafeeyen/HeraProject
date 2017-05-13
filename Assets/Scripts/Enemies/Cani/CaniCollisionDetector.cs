using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaniCollisionDetector : MonoBehaviour {

	private CanipalntAIController controller;

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
