using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlankItem : Items
{
	private string itemName = "blank";
    private string itemDescription = "(No Description)";
    private int itemQuantity = 0;
    private bool stack = false;

	public BlankItem() {}

    public string names
    {
        get { return itemName; }
        set { itemName = value; }
    }

    public string description
    {
        get { return itemDescription; }
        set { itemDescription = value; }
    }

    public int quantity
    {
        get { return itemQuantity; }
        set { itemQuantity = value; }
    }

    public bool stackAble
    {
        get { return stack; }

        set { stack = value; }
    }
}

