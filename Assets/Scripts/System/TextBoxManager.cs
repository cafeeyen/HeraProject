using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {
	public GameObject textBox;
	public Text theText, speakerName;
	public TextAsset textFile;
	public string[] textLines;

	public int currentLine, endAtLine;
	public bool isTextboxActive;
	public HeraControl player;
	

	// Use this for initialization
	void Start () 
	{
		//player = FindObjectOfType<CharacterControl>();
		if(textFile != null)
		{
			textLines = (textFile.text.Split('\n'));
		}

		if(endAtLine == 0)
		{
			endAtLine = textLines.Length - 1;
		}

		
		
		if(isTextboxActive){enableTextBox();}
		else{disableTextBox();}
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.FindWithTag("Player").GetComponent<HeraControl>();
		if(!isTextboxActive)
		{
			return;
		}
		
		if(!(currentLine > endAtLine)){
			theText.text = textLines[currentLine].Split('|')[1];
			speakerName.text = textLines[currentLine].Split('|')[0];
		}

		if(currentLine > endAtLine){
			disableTextBox();
		}

		if(Input.GetKeyDown(KeyCode.Space) && isTextboxActive && Time.timeScale != 0)
        {
			currentLine += 1;
		}

	}

	public void enableTextBox()
	{
		isTextboxActive = true;
		textBox.SetActive(true);
		player.enabled = false;		
	}

	public void disableTextBox()
	{
		isTextboxActive = false;
		textBox.SetActive(false);
		player.enabled = true;
	}

	public void reloadScript(TextAsset theText)
	{
		if(theText != null)
		{
			textLines = new string[1];
			textLines = (theText.text.Split('\n'));
		}
	}
}
