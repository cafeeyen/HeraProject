using UnityEngine;

public class HarpyRedCollisionDetector : MonoBehaviour
{

	private HarpyRedAIController controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
		{
            controller = transform.root.GetComponent<HarpyRedAIController>();

            if (transform.gameObject.name.Equals("HarpyRedHead"))
            {
                controller.IsColliding = true;
                controller.Action = "HarpyRed_Head";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (transform.gameObject.name.Equals("HarpyRedHead"))
                controller.IsColliding = false;
        }
    }
}
