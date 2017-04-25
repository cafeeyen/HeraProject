using UnityEngine;

[System.Serializable]
public class Item01 :  Items
{
    private string itemName = "Item01";
    private string itemDescription = "น้ำโง่ๆธรรมดา ที่เด้งดึ๋ง?";
    private int itemQuantity = 0;
    private bool stack = true;

    public Item01() {}

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
