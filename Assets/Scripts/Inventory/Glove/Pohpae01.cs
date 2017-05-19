using UnityEngine;

[System.Serializable]
public class Pohpae01 :  Equipment
{
	private int iid;
	private int iatk = 999, idef = 20, ihp = 80;
	public Rarity irarity = Rarity.Legendary;
	public EquipmentType iequipmentType = EquipmentType.Glove;
	private string itemName = "Poh-Pae";
    private string itemDescription = "popepopoeppppepepepepepepepepepeeeee";
    private int itemQuantity = 0;
    private bool stack = false;

	public Pohpae01(int gid)
	{
		iid = gid;
		// iid = GameData.data.itemID;
		// GameData.data.itemID += 1; 
	}

	public Pohpae01(Rarity irarity)
	{
        iatk = iatk * (int)(((int)irarity + 1 * Random.Range(0.8f, 1.1f)) * Random.Range(0.6f, 1.5f));
        idef = idef * (int)(((int)irarity + 1 * Random.Range(0.8f, 1.1f)) * Random.Range(0.6f, 1.5f));
        ihp = ihp * (int)(((int)irarity + 1 * Random.Range(0.8f, 1.1f)) * Random.Range(0.6f, 1.5f));
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

	public int atk
    {
        get { return iatk; }
        set { iatk = value; }
    }

	public int def
    {
        get { return idef; }
        set { idef = value; }
    }

	public int hp
    {
        get { return ihp; }
        set { ihp = value; }
    }

    public bool stackAble
    {
        get { return stack; }

        set { stack = value; }
    }
}

