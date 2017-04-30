using UnityEngine;

public class NguaCollisionDetector : MonoBehaviour
{
    private NguaAIController controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
		{
            controller = transform.root.GetComponent<NguaAIController>();

            if (transform.gameObject.name.Equals("NguaHead"))
            {
                controller.IsColliding = true;
                controller.Action = "Ngua_Head";
            }
            else if (transform.gameObject.name.Equals("NguaLArm") || transform.gameObject.name.Equals("NguaRArm"))
            {
                controller.Action = "Ngua_Slap";
            }
            else if (transform.gameObject.name.Equals("NguaTail"))
            {
                controller.Action = "Ngua_Tail";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
		{
            if (transform.gameObject.name.Equals("NguaHead"))
                controller.IsColliding = false;
        }
    }
}
