using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Items> pIList; //Player Item List
    public static PlayerInventory inventory;
    public int INVENTORY_SIZE = 20, changeIndex = -1;

    public PlayerInventory()
    {
        pIList = new List<Items>();
    }

    public void createBlankItem()
    {
        Debug.Log("createBlankItem");
        for (int i = 0; i < INVENTORY_SIZE; i++)
        {
            pIList.Add(new BlankItem());
        }
        //Debug.Log(pIList.Count);
    }

    //add item to Player inventory
    public void addItem(Items item)
    {
        //Stackable and already have item
        if (item.stackAble && pIList.FindIndex(a => a.names == item.names) != -1)
        {
            findItemFromName(item.names).quantity += 1;
        }
        else //Can't stackable or not have item in list
        {
            for (int i = 0; i < pIList.Count; i++)
            {
                if (pIList[i].names == "blank")
                {
                    pIList[i] = item;
                    changeIndex = i;
                    break;
                }
            }
        }
        //printAllItem();
    }

    //not test now
    public void removeItem(Items item, int Outquant = 1)  //may return boolean
    {
        if (pIList.Contains(item))
        {
            int nowAmount = findItemFromName(item.names).quantity;
            if (nowAmount >= Outquant)
            {
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
}
