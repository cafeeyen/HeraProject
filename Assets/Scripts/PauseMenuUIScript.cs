using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUIScript : MonoBehaviour {

	public SaveLoadUtility slu;
	//public CharacterControl player;
	public GameObject startMenu, saveMenu, loadMenu;
	public Button loadSlot1, loadSlot2, loadSlot3, saveSlot1, saveSlot2, saveSlot3;
	public Text loadTextSlot1, loadTextSlot2, loadTextSlot3, saveTextSlot1, saveTextSlot2, saveTextSlot3;

 	public List<SaveGame> saveGames;
	public bool isStartMenuActive = false;

	private string saveGameName;
	private int SLOT_SIZE = 3;
	private bool[] slotHasData = new bool[3]{false, false, false};

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(saveMenu.activeSelf){saveMenu.SetActive(false); enableStartMenu();}
			else if(loadMenu.activeSelf){loadMenu.SetActive(false); enableStartMenu();}
			else if(!isStartMenuActive){enableStartMenu();}			
			else{disableStartMenu();}
		}
	}

	public void enableStartMenu()
	{
		isStartMenuActive = true;
		startMenu.SetActive(true);
	}

	public void disableStartMenu()
	{
		isStartMenuActive = false;
		startMenu.SetActive(false);
	}


	// Save Button menu function
	public void savePressed()
	{
		saveGames = SaveLoad.GetSaveGames(slu.saveGamePath, slu.usePersistentDataPath);
		saveMenu.SetActive(true);
		disableStartMenu();

		foreach(SaveGame saveGame in saveGames) 
		{
			if(string.Equals(saveGame.savegameName, "Slot1"))
				{saveTextSlot1.text = (saveGame.savegameName + ": " + saveGame.saveDate); slotHasData[0] = true;}
			if(string.Equals(saveGame.savegameName, "Slot2"))
				{saveTextSlot2.text = (saveGame.savegameName + ": " + saveGame.saveDate); slotHasData[1] = true;}
			if(string.Equals(saveGame.savegameName, "Slot3"))
				{saveTextSlot3.text = (saveGame.savegameName + ": " + saveGame.saveDate); slotHasData[2] = true;}
		}

		for (int datanum = 0; datanum < slotHasData.Length; datanum++)
		{
			if(!slotHasData[datanum])
			{
				getSaveTextSlot(datanum+1).text = "Slot" + (datanum+1) + " (Blank Slot)";
			}
		}
	}
	public void SaveSlot(int slot)
	{
		saveGameName = "Slot"+slot;
		slu.SaveGame(saveGameName);
		getSaveTextSlot(slot).text = (saveGameName + ": " + DateTime.Now.ToString());
		slotHasData = new bool[3]{false, false, false};
	}
	public void saveBackPress()
	{
		saveMenu.SetActive(false);
		enableStartMenu();
	}




	//Load button menu function
	public void loadPressed()
	{
		saveGames = SaveLoad.GetSaveGames(slu.saveGamePath, slu.usePersistentDataPath);

		loadMenu.SetActive(true);
		disableStartMenu();

		foreach(SaveGame saveGame in saveGames) 
		{
			Debug.Log(saveGame.savegameName + "---" + saveGame.saveDate);
			if(string.Equals(saveGame.savegameName, "Slot1"))
				{loadTextSlot1.text = (saveGame.savegameName + ": " + saveGame.saveDate); slotHasData[0] = true;}
			if(string.Equals(saveGame.savegameName, "Slot2"))
				{loadTextSlot2.text = (saveGame.savegameName + ": " + saveGame.saveDate); slotHasData[1] = true;}
			if(string.Equals(saveGame.savegameName, "Slot3"))
				{loadTextSlot3.text = (saveGame.savegameName + ": " + saveGame.saveDate); slotHasData[2] = true;}
		}
		for (int datanum = 0; datanum < slotHasData.Length; datanum++)
		{
			if(!slotHasData[datanum])
			{
				getLoadButtonSlot(datanum+1).interactable = false; 
				getLoadTextSlot(datanum+1).text = "(Blank Slot)";
			}
		}
	}

	public void loadSlot(int num)
	{
		foreach(SaveGame saveGame in saveGames) 
		{
			if(string.Equals(saveGame.savegameName, ("Slot"+num))) {slu.LoadGame(saveGame.savegameName);}
		}
	}
	
	public void loadBackPress()
	{
		loadMenu.SetActive(false);
		enableStartMenu();
	}

	private Text getLoadTextSlot(int num)
	{
		if(num == 1){return loadTextSlot1;}
		else if(num == 2){return loadTextSlot2;}
		else if(num == 3){return loadTextSlot3;}
		return null;
	}

	private Button getLoadButtonSlot(int num)
	{
		if(num == 1){return loadSlot1;}
		else if(num == 2){return loadSlot2;}
		else if(num == 3){return loadSlot3;}
		return null;
	}

	private Text getSaveTextSlot(int num)
	{
		if(num == 1){return saveTextSlot1;}
		else if(num == 2){return saveTextSlot2;}
		else if(num == 3){return saveTextSlot3;}
		return null;
	}

	private Button getSaveButtonSlot(int num)
	{
		if(num == 1){return saveSlot1;}
		else if(num == 2){return saveSlot2;}
		else if(num == 3){return saveSlot3;}
		return null;
	}

	public IEnumerator Wait(float delayInSecs)
	{
		yield return new WaitForSeconds(delayInSecs);
	}
}
