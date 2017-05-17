using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform canvas;
    private GameObject player;

    void Start ()
    {
		player = GameObject.FindWithTag("Player");
	}

	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (canvas.gameObject.activeInHierarchy == false)
            {
                canvas.gameObject.SetActive(true);
                // Pause
                Time.timeScale = 0;
                //player.gameObject.GetComponent<CharacterController>().enabled = false;
            }
            else
            {
                canvas.gameObject.SetActive(false);
                // Resume
                Time.timeScale = 1;
                //player.gameObject.GetComponent<CharacterController>().enabled = true;
            }
        }
    }
}
