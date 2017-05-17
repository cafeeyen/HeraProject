using UnityEngine;

public class GolemCollisionDetector : MonoBehaviour {

	private GolemAIController controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
		{
            controller = transform.root.GetComponent<GolemAIController>();

            if (transform.gameObject.name.Equals("GolemRHand") || transform.gameObject.name.Equals("GolemLHand"))
            {
                controller.Action = "Golem_Hand";
            }
        }
    }
}
