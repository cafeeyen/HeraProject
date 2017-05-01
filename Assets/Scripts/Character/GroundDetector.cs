using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour {

	private HeraControl heracontrol;

	private void OnTriggerEnter(Collider other)
    {
		heracontrol = transform.root.GetComponent<HeraControl>();
		if (other.gameObject.CompareTag("Terrain") || other.gameObject.CompareTag("Ground"))
		{
			heracontrol.IsGround = true;
		}
	}

	private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Terrain") || other.gameObject.CompareTag("Ground"))
		{
			heracontrol.IsGround = false;
		}
    }
}
