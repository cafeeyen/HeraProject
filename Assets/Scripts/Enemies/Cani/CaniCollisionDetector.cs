using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaniCollisionDetector : MonoBehaviour {

	private CanipalntAIController controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
		{
            controller = transform.root.GetComponent<CanipalntAIController>();

            if (transform.gameObject.name.Equals("CaniBite"))
            {
                controller.Action = "Cani_Bite";
            }
            else if (transform.gameObject.name.Equals("CaniLTentacle") || transform.gameObject.name.Equals("CaniRTentacle"))
            {
                controller.Action = "Cani_Tentacle";
            }
        }
    }
}
