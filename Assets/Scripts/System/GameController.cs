using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform canvas;
    public Transform player;

	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (canvas.gameObject.activeInHierarchy == false)
            {
                canvas.gameObject.SetActive(true);
                // Pause
                Time.timeScale = 0;
                player.GetComponent<CharacterController>().enabled = false;
            }
            else
            {
                canvas.gameObject.SetActive(false);
                // Resume
                Time.timeScale = 1;
                player.GetComponent<CharacterController>().enabled = true;
            }
        }
    }
}
