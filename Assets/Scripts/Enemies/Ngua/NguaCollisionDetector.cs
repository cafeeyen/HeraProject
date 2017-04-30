using UnityEngine;

public class NguaCollisionDetector : MonoBehaviour
{

	public static bool isColliding = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
		{
            if (transform.gameObject.name.Equals("NguaHead"))
            {
                isColliding = true;
                Debug.Log("Ouch!" + " " + transform.gameObject.name);
            }
            else if (transform.gameObject.name.Equals("NguaArm"))
            {
                Debug.Log("Ouch!" + " " + transform.gameObject.name);
            }
            else if (transform.gameObject.name.Equals("NguaTail"))
            {
                Debug.Log("Ouch!" + " " + transform.gameObject.name);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
		{
            if (transform.gameObject.name.Equals("NguaHead"))
                isColliding = false;
        }
    }
}
