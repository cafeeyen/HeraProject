using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextAtLine : MonoBehaviour {

	public TextAsset theText;
	public TextBoxManager textBoxManager;
	public int startLine, endLine;


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
			Debug.Log("trig");
			textBoxManager.reloadScript(theText);
			textBoxManager.currentLine = startLine;
			textBoxManager.endAtLine = endLine;
			textBoxManager.enableTextBox();
		}
    }
}
