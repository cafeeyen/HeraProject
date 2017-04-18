using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterWindow : MonoBehaviour {

	//now playerinventory can't access characterwindow
	public GameObject characterMenu, slotPanel, sslot, toolTipPanel; // inventorySlot, inventoryItem
	public Image itemGhost, hatImage, groveImage, suitImage;
	public Text toolTipItemName, toolTipItemType, toolTipRarity, toolTipDescription, toolTipHP, toolTipAtk, toolTipDef;
	public List<GameObject> slotsList = new List<GameObject>();

	int slotAmount, slotAt;

	public static bool isMouseOver = false, releaseDrag = false, dragging = false, overSpecialSlot = false;
	public static Items currentItem;
	public static int dragItem, nowOver;
	public static string putOver = "";

	//public List<Items> itemsInBox = new List<Items>();
	PlayerInventory pinv;
	bool isCharacterMenuActive = false;

	// Use this for initialization
	void Start () {
		pinv = GetComponent<PlayerInventory>();
		pinv = PlayerInventory.inventory;

		currentItem = new BlankItem();

		slotAmount = 16;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E))
		{
			if(!isCharacterMenuActive){enableCharacterMenu(); addAllItemToSlot();}
			else{disableCharacterMenu(); isMouseOver = false;}

		}
		if(isMouseOver)
		{
			toolTipPanel.SetActive(true);
			toolTipPanel.transform.position = Input.mousePosition + new Vector3(50,-100f,0f);
			toolTipItemName.text = currentItem.names;
			toolTipDescription.text = "Description:" + currentItem.description;
			if(currentItem is Equipment)
			{
				Equipment eq = (Equipment)currentItem;
				toolTipItemType.text = "Type:  " + eq.equipmentType.ToString();
				toolTipRarity.text = "Rarity: " + eq.rarity.ToString();
				toolTipHP.text = "HP: " + eq.hp;
				toolTipAtk.text = "Atk: " +  eq.atk;
				toolTipDef.text = "Def: " + eq.def;
			}
			//Debug.Log(Input.mousePosition);
		}
		else if(!isMouseOver){toolTipPanel.SetActive(false);}
		
		if(dragging && pinv.pIList[dragItem].names != "blank")
		{
			toolTipPanel.SetActive(false);
			itemGhost.enabled = true;
			itemGhost.sprite = pinv.pIList[dragItem].icon;
			itemGhost.transform.position = Input.mousePosition + new Vector3(10f,-10f,0f);
		}
		else if(!dragging){itemGhost.enabled = false;}

		if(releaseDrag && overSpecialSlot)
		{
			if(putOver == "Trash")
			{
				deleteItem(dragItem); 
				refreshWindow();
			}
			else if(pinv.pIList[dragItem] is Equipment)
			{
				Equipment eq = (Equipment)pinv.pIList[dragItem];
				if(putOver == eq.equipmentType.ToString())
				{
					if(putOver == EquipmentType.Hat.ToString()){hatImage.sprite = eq.icon;}
					else if(putOver == EquipmentType.Glove.ToString()){groveImage.sprite = eq.icon;}
					else if(putOver == EquipmentType.Suit.ToString()){suitImage.sprite = eq.icon;}
					Debug.Log("Equiped:" + pinv.pIList[dragItem].names + "     AT " + putOver);
				}
			}
		}

		if(releaseDrag && dragItem != nowOver)
		{
			swapItem(dragItem, nowOver);
			releaseDrag = false;
			refreshWindow();
		}
		releaseDrag = false;
	}

	//player list zone
	public void addAllItemToSlot()
	{
		slotAt = 0;
		for (int i = 0; i < 16; i++)//pinv.pIList.Count
		{
			GameObject slot = (GameObject)Instantiate(sslot);
			slot.name = "Slot" + i;
			slot.GetComponent<SlotScript>().slotNumber = slotAt;
			slotsList.Add(slot);
			slot.transform.SetParent(slotPanel.gameObject.transform, false);
			if(i < pinv.pIList.Count) //put item to this tile
			{
				//Debug.Log(pinv.pIList[i].names);
				slot.GetComponent<SlotScript>().item = pinv.pIList[i];
			}
			slotAt += 1;
		}
	}

	public void swapItem(int swapfrom, int swapto)
	{
		Items temp = new BlankItem();
		temp = pinv.pIList[swapfrom];
		pinv.pIList[swapfrom] = pinv.pIList[swapto];
		pinv.pIList[swapto] = temp;
	}

	public void deleteItem(int deleteIndex)
	{
		pinv.pIList[deleteIndex] = new BlankItem();
	}


	//UI Zone
	public void refreshWindow()
	{
		disableCharacterMenu(); 
		isMouseOver = false;
		enableCharacterMenu(); 
		addAllItemToSlot();
	}
	
	public void enableCharacterMenu()
	{
		isCharacterMenuActive = true;
		characterMenu.SetActive(true);
	}

	public void disableCharacterMenu()
	{
		isCharacterMenuActive = false;
		foreach (Transform child in slotPanel.transform) //this code may Over Allocate memory
        {
            Destroy(child.gameObject);
        }
		characterMenu.SetActive(false);
	}

/* 
	public void addItem(string itemname)
	{
		Debug.Log("Call Add Item");
		// Items itemToAdd = pinv.findItemFromName(itemname);
		// for (int i = 0; i < itemsInBox.Count; i++)
		// {
		// 	itemsInBox[i] = itemToAdd;
		// 	GameObject itemObj = Instantiate(inventoryItem);
		// 	itemObj.transform.SetParent(slots[i].transform);
		// 	itemObj.transform.position = Vector2.zero;
		// 	Debug.Log("Added " +itemsInBox[i].names + " at" + i);
		// }
	}
*/


		//inventoryPanel = GameObject.Find("Inventory Panel");
		//slotPanel = inventoryPanel.transform.FindChild("SlotPanel").gameObject;

/*		// paste blank slot box
		for (int i = 0; i < slotAmount; i++)
		{
			//itemsInBox.Add(new Items());
			slots.Add(Instantiate(inventorySlot));
			slots[i].transform.SetParent(slotPanel.transform);
		}
*/
}
