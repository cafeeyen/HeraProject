using UnityEngine;

[System.Serializable]
public class PaperHat : Equipment
{
    private int iid;
    private float iatk = 0, idef = 2, ihp = 4;
    public Rarity irarity;
    public EquipmentType iequipmentType = EquipmentType.Hat;
    private string itemName = "Paper Hat";
    private string itemDescription = "A4 Paper hat. Nothing special.";
    private int itemQuantity = 0;
    private bool stack = false;

    public PaperHat(int gid)
    {
        iid = gid;
        // iid = GameData.data.itemID;
        // GameData.data.itemID += 1; 
    }

    public PaperHat()
    {
        irarity = (Rarity)Random.Range(0, 3);
        iatk = iatk * ((int)irarity + 1 * Random.Range(0.8f, 1.1f)) * Random.Range(0.6f, 1.5f);
        idef = idef * ((int)irarity + 1 * Random.Range(0.8f, 1.1f)) * Random.Range(0.6f, 1.5f);
        ihp = ihp * ((int)irarity + 1 * Random.Range(0.8f, 1.1f)) * Random.Range(0.6f, 1.5f);
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
}
