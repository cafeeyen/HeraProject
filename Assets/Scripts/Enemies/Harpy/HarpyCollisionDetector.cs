using UnityEngine;

public class HarpyCollisionDetector : MonoBehaviour
{

	private HarpyAIController controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
		{
            controller = transform.root.GetComponent<HarpyAIController>();

            if (transform.gameObject.name.Equals("HarpyHead"))
            {
                controller.IsColliding = true;
                controller.Action = "Harpy_Head";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (transform.gameObject.name.Equals("HarpyHead"))
                controller.IsColliding = false;
        }
    }
}
