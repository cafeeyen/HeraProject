using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterWindow : MonoBehaviour
{
    /*
     * Now playerinventory can't access characterwindow.
     * Since dragItem don't have default value/reset value, they need to stay lower then spDragItem in case of priority.
    */
    public GameObject characterMenu, slotPanel, sslot, toolTipPanel, hatSlot, gloveSlot, suitSlot; // inventorySlot, inventoryItem
    public Image itemGhost;
    public Text toolTipItemName, toolTipItemType, toolTipRarity, toolTipDescription, toolTipHP, toolTipAtk, toolTipDef;
    public List<GameObject> slotsList = new List<GameObject>();

    public static bool isMouseOver = false, releaseDrag = false, dragging = false, overSpecialSlot = false;
    public static Items currentItem, spDragItem;
    public static int dragItem, nowOver;
    public static string putOver = "";
    public PlayerInventory pinv;

    void Start()
    {
        pinv = GameData.data.inventory;
        currentItem = new BlankItem();
        setEmptySlot();
    }

    void Update()
    {
        if (pinv.changeIndex != -1)
        {
            changeItem(pinv.changeIndex);
            pinv.changeIndex = -1;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            characterMenu.SetActive(!characterMenu.activeInHierarchy);
            if (!characterMenu.activeInHierarchy) isMouseOver = false;
        }

        if (isMouseOver && !(currentItem is BlankItem))
        {
            toolTipPanel.SetActive(true);
            toolTipPanel.transform.position = Input.mousePosition + new Vector3(50, -100f, 0f);
            toolTipItemName.text = currentItem.names;
            toolTipDescription.text = "Description:" + currentItem.description;
            if (currentItem is Equipment)
            {
                Equipment eq = (Equipment)currentItem;
                toolTipItemType.text = "Type:  " + eq.equipmentType.ToString();
                toolTipRarity.text = "Rarity: " + eq.rarity.ToString();
                toolTipHP.text = "HP: " + eq.hp;
                toolTipAtk.text = "Atk: " + eq.atk;
                toolTipDef.text = "Def: " + eq.def;
            }
        }
        else if (!isMouseOver) toolTipPanel.SetActive(false);

        if (dragging)
        {
            toolTipPanel.SetActive(false);
            itemGhost.enabled = true;
            itemGhost.transform.position = Input.mousePosition + new Vector3(10f, -10f, 0f);
            if (spDragItem != null && !(spDragItem is BlankItem)) itemGhost.sprite = Resources.Load<Sprite>(spDragItem.names); // Drag from equipment
            else if (spDragItem == null && pinv.pIList[dragItem].names != "blank") itemGhost.sprite = Resources.Load<Sprite>(pinv.pIList[dragItem].names); // Drag fron inventory
            else itemGhost.enabled = false;
        }
        else itemGhost.enabled = false;

        if (releaseDrag)
        {
            // Unequip(?)
            if (spDragItem != null && !spDragItem.names.Equals("blank") && !overSpecialSlot && isMouseOver)
            {
                Equipment eq = (Equipment)spDragItem;
                // Swap same type equipment
                if (pinv.pIList[nowOver] is Equipment)
                {
                    Equipment inBag = (Equipment)pinv.pIList[nowOver];
                    if (inBag.equipmentType.Equals(eq.equipmentType)) swapEquipItem(eq);
                }
                // Send equipment to empty slot
                else if(pinv.pIList[nowOver] is BlankItem) swapEquipItem(eq);
            }
            // Equip / Trash
            else if (overSpecialSlot)
            {
                if (putOver == "Trash" && spDragItem == null) deleteItem(dragItem); // Delete item(Can't delete current equip item)
                else if (pinv.pIList[dragItem] is Equipment) // Equip item
                {
                    Equipment eq = (Equipment)pinv.pIList[dragItem];
                    if (putOver == eq.equipmentType.ToString())
                    {
                        if (putOver == EquipmentType.Hat.ToString()) addEquipment(hatSlot, eq, dragItem);
                        else if (putOver == EquipmentType.Glove.ToString()) addEquipment(gloveSlot, eq, dragItem);
                        else if (putOver == EquipmentType.Suit.ToString()) addEquipment(suitSlot, eq, dragItem);
                    }
                }
            }
            // In inventory only(Swap)
            else if (dragItem != nowOver)
            {
                swapItem(dragItem, nowOver);
                releaseDrag = false;
            }

            spDragItem = null;
            releaseDrag = false;
        }
    }

    //Player list zone
    private void setEmptySlot()
    {
        for (int i = 0; i < pinv.INVENTORY_SIZE; i++)
        {
            GameObject slot = (GameObject)Instantiate(sslot);
            slot.name = "Slot" + i;
            slot.GetComponent<SlotScript>().slotNumber = i;
            slotsList.Add(slot);
            slot.transform.SetParent(slotPanel.gameObject.transform, false);
            slotsList[i].GetComponent<SlotScript>().item = pinv.pIList[i];
        }
    }

    private void addEquipment(GameObject slot, Equipment eq, int bagIndex)
    {
        SlotEquipmentScript sl = slot.GetComponent<SlotEquipmentScript>();

        pinv.pIList[bagIndex] = sl.item;
        pinv.changeIndex = bagIndex;

        sl.item = eq;
        sl.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(eq.names);
    }

    private void swapItem(int swapfrom, int swapto)
    {
        Items temp = new BlankItem();
        temp = pinv.pIList[swapfrom];
        pinv.pIList[swapfrom] = pinv.pIList[swapto];
        pinv.pIList[swapto] = temp;

        changeItem(swapfrom);
        changeItem(swapto);
    }

    private void deleteItem(int deleteIndex)
    {
        pinv.pIList[deleteIndex] = new BlankItem();
        changeItem(deleteIndex);
    }

    private void changeItem(int index)
    {
        //Change item in that slot
        slotsList[index].GetComponent<SlotScript>().item = pinv.pIList[index];
    }

    private void swapEquipItem(Equipment eq)
    {
        Items temp = pinv.pIList[nowOver];
        pinv.pIList[nowOver] = eq;
        pinv.changeIndex = nowOver;
        switch (eq.equipmentType)
        {
            case (EquipmentType.Hat):
                hatSlot.GetComponent<SlotEquipmentScript>().item = temp;
                hatSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(temp.names);
                break;

            case (EquipmentType.Suit):
                suitSlot.GetComponent<SlotEquipmentScript>().item = temp;
                suitSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(temp.names);
                break;

            case (EquipmentType.Glove):
                gloveSlot.GetComponent<SlotEquipmentScript>().item = temp;
                gloveSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(temp.names);
                break;
        }
    }
}
