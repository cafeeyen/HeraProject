using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

	//CharacterWindow characterWindow;
	public List<Items> pIList; //Player Item List
	public static PlayerInventory inventory;
	public int INVENTORY_SIZE = 16;

	public PlayerInventory()
	{
		//characterWindow = GameObject.Find("CharacterWindowEmptyObject").GetComponent<CharacterWindow>();
		pIList = new List<Items>();
	}

	public void createBlankItem()
	{
		//Debug.Log("create blank");
		for (int i = 0; i < INVENTORY_SIZE; i++)
		{
			pIList.Add(new BlankItem());
		}
		Debug.Log(pIList.Count);
	}

	//add item to hera inventory
	public void addItem(Items item)
	{
		//if(pIList.FindIndex(a => a.names == item.names) == -1 ) //&& string.Equals(item.names, "Helmet01"){}

		if(item.stackAble && pIList.FindIndex(a => a.names == item.names) != -1 ) //stackable and already have item
		{
			findItemFromName(item.names).quantity +=  1;
		}
		else //can't stackable or not have item in list
		{
			for (int i = 0; i < pIList.Count; i++)
			{
				if(pIList[i].names == "blank")
				{
					pIList[i] = item;
					break;
				}
			}
		}
		printAllItem();
	}

	//not test now
	public void removeItem(Items item, int Outquant = 1)  //may return boolean
	{  
		if(pIList.Contains(item)){
			int nowAmount = findItemFromName(item.names).quantity;
			if(nowAmount >= Outquant){
				pIList[pIList.IndexOf(item)].quantity = nowAmount - Outquant;
			}
		}
	}

	public Items findItemFromName(string name)
	{
		return pIList[pIList.FindIndex(a => a.names == name)];
	}


	public void printAllItem()
	{
		string text = " ";
		foreach (Items it in pIList)
		{
			text += string.Format("{0}x{1}, ", it.names, it.quantity);
		}
		Debug.Log("Now have Item: " + text);
	}

	// public boolean IsPlayerHasItemEquals(string itemName, int number){ 
	// 	if(playerInventory.ContainsKey(itemName)){
	// 		if(playerInventory[itemName] >= number){
	// 			return true;
	// 		}
	// 	}
	// 	return false;
	// }

}
