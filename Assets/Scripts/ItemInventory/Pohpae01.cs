using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pohpae01 :  Equipment {

	private int iid;
	private float iatk = 999, idef = 20, ihp = 80;
	public Rarity irarity = Rarity.Legendary;
	public EquipmentType iequipmentType = EquipmentType.Glove;
	private string itemName = "Poh-Pae";
    private string itemDescription = "popepopoeppppepepepepepepepepepeeeee";
    private int itemQuantity = 0;
    private bool stack = false;
    private Sprite iconPic = Resources.Load<Sprite>("pohpae");

	public Pohpae01(int gid)
	{
		iid = gid;
		// iid = GameData.data.itemID;
		// GameData.data.itemID += 1; 
	}

	public Pohpae01()
	{
	}


	//getter setter
	public int id
    {
        get { return iid; }
        set { iid = value; }
    }
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

	public Rarity rarity
    {
        get { return irarity; }
        set { irarity = value; }
    }

	public EquipmentType equipmentType
    {
        get { return iequipmentType; }
        set { iequipmentType = value; }
    }

	public float atk
    {
        get { return iatk; }
        set { iatk = value; }
    }

	public float def
    {
        get { return idef; }
        set { idef = value; }
    }

	public float hp
    {
        get { return ihp; }
        set { ihp = value; }
    }

    public bool stackAble
    {
        get { return stack; }

        set { stack = value; }
    }

    public Sprite icon
    {
        get { return iconPic; }

        set { iconPic = value; }
    }
}

