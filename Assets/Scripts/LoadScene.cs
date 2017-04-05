using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	public int scene;

	// Use this for initialization
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
			SceneManager.LoadScene(scene);
		}
	}
}
