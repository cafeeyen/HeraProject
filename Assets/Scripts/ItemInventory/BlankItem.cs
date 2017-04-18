using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankItem :  Equipment {

	private int iid;
	private float iatk = 0, idef = 0, ihp = 0;
	public Rarity irarity = Rarity.Normal;
	public EquipmentType iequipmentType = EquipmentType.None;
	private string itemName = "blank";
    private string itemDescription = "(No Description)";
    private int itemQuantity = 0;
    private bool stack = false;
    private Sprite iconPic = Resources.Load<Sprite>("blankItem");

	public BlankItem(int gid)
	{
		iid = gid;
		// iid = GameData.data.itemID;
		// GameData.data.itemID += 1; 
	}

	public BlankItem()
	{
	}

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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

