using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item01 :  Items
{
    public Item01(){}

    private string itemName = "Item01";
    private string itemDescription = "น้ำโง่ๆธรรมดา ที่เด้งดึ๋ง?";
    private int itemQuantity = 0;
    private Sprite iconPic = Resources.Load<Sprite>("item01");
    private bool stack = true;

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

    public Sprite icon
    {
        get { return iconPic; }

        set { iconPic = value; }
    }

    public bool stackAble
    {
        get { return stack; }

        set { stack = value; }
    }

}
