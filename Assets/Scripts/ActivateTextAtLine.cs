using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextAtLine : MonoBehaviour {

	public TextAsset theText;
	public TextBoxManager textBoxManager;
	public int startLine, endLine;
	public bool destroyOnEnd = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
		{
			textBoxManager.reloadScript(theText);
			textBoxManager.currentLine = startLine;
			textBoxManager.endAtLine = endLine;
			textBoxManager.enableTextBox();
		}
    }

	private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && destroyOnEnd)
		{
			Destroy(gameObject);
		}
    }
}
