using UnityEngine;

public class CyclopCollisionDetector : MonoBehaviour {

	private CyclopAIControl controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
		{
            controller = transform.root.GetComponent<CyclopAIControl>();

            if (transform.gameObject.name.Equals("CyclopHead"))
            {
                controller.IsColliding = true;
                controller.Action = "Cyclop_Head";
            }
            else if (transform.gameObject.name.Equals("CyclopClub"))
            {
                controller.Action = "Cyclop_Club";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
		{
            if (transform.gameObject.name.Equals("CyclopHead"))
                controller.IsColliding = false;
        }
    }
}
