using UnityEngine;

public class MiaNoiCollisionDetector : MonoBehaviour {

	private MiaNoiAIController controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
		{
            controller = transform.root.GetComponent<MiaNoiAIController>();

            if (transform.gameObject.name.Equals("LeftHandCollider"))
            {
                controller.IsColliding = true;
                controller.Action = "MiaNoi_Hand";
            }
            else if (transform.gameObject.name.Equals("PanCollider"))
            {
                controller.Action = "MiaNoi_Pan";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
		{
            if (transform.gameObject.name.Equals("LeftHandCollider"))
                controller.IsColliding = false;
        }
    }
}

