using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DieRespawnUI : MonoBehaviour {

	public Image dieUI;

	// Use this for initialization
	void Start () {
		dieUI.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(GameData.data.curHp <= 0)
		{
			dieUI.gameObject.SetActive(true);
		}
	}
}
