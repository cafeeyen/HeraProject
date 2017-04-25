using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleController : MonoBehaviour
{

	public Transform sparkle;

	// Use this for initialization
	void Start ()
    {
		sparkle.GetComponent<ParticleSystem>().Stop();
	}
	
	public void playParticle(Color color = new Color())
	{
		sparkle.GetComponent<ParticleSystem>().Play();
		sparkle.GetComponent<ParticleSystem>().startColor = color;
	}

	IEnumerator wait(float times)
	{
		yield return new WaitForSeconds(times);
	}
}
