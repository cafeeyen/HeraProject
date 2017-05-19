using UnityEngine;

public class HeraCollisionDetector : MonoBehaviour
{
    private HeraControl controller;

	void Start ()
    {
        controller = transform.root.GetComponent<HeraControl>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (controller.Action.Equals("Comboing") || controller.Action.Equals("Kicking") || controller.Action.Equals("Slaping"))
        {
            if(other.tag.Equals("Enemy") || other.tag.Equals("Boss"))
            {
                DamageSystem.DamagetoEnemy(controller.Action, controller.ComboMove, other.transform.root.gameObject);
            }
        }
            
    }
}
