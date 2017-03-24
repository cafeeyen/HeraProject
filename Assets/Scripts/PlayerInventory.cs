using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

	private List<Items> playerInventory= new List<Items>();

	// Use this for initialization
	void Start () {
		playerInventory.Add(new Item01());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addItem(Items item){
		playerInventory[playerInventory.FindIndex(a => a.names == item.names)].quantity +=  1;
		printAllItem();
	}

	public void removeItem(Items item, int Outquant = 1){  //may return boolean
		if(playerInventory.Contains(item)){
			int nowAmout = playerInventory[playerInventory.FindIndex(a => a.names == item.names)].quantity;
			if(nowAmout >= Outquant){
				playerInventory[playerInventory.IndexOf(item)].quantity = nowAmout - Outquant;
			}
		}
	}

	public void printAllItem(){
		string text = " ";
		foreach (Items it in playerInventory)
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
