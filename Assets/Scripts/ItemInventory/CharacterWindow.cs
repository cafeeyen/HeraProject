using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterWindow : MonoBehaviour
{

    //Now playerinventory can't access characterwindow
    public GameObject characterMenu, slotPanel, sslot, toolTipPanel; // inventorySlot, inventoryItem
    public Image itemGhost, hatImage, groveImage, suitImage;
    public Text toolTipItemName, toolTipItemType, toolTipRarity, toolTipDescription, toolTipHP, toolTipAtk, toolTipDef;
    public List<GameObject> slotsList = new List<GameObject>();

    public static bool isMouseOver = false, releaseDrag = false, dragging = false, overSpecialSlot = false;
    public static Items currentItem;
    public static int dragItem, nowOver;
    public static string putOver = "";
    public PlayerInventory pinv;

    void Start()
    {
        pinv = GetComponent<PlayerInventory>();
        pinv = PlayerInventory.inventory;

        currentItem = new BlankItem();
        setEmptySlot();
    }

    void Update()
    {
        if (pinv.addIndex != -1)
        {
            changeItem(pinv.addIndex);
            pinv.addIndex = -1;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            characterMenu.SetActive(!characterMenu.activeInHierarchy);
            if (!characterMenu.activeInHierarchy) isMouseOver = false;
        }

        if (isMouseOver)
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

        if (dragging && pinv.pIList[dragItem].names != "blank")
        {
            toolTipPanel.SetActive(false);
            itemGhost.enabled = true;
            itemGhost.sprite = pinv.pIList[dragItem].icon;
            itemGhost.transform.position = Input.mousePosition + new Vector3(10f, -10f, 0f);
        }
        else if (!dragging) { itemGhost.enabled = false; }

        if (releaseDrag && overSpecialSlot)
        {
            if (putOver == "Trash")  deleteItem(dragItem);
            else if (pinv.pIList[dragItem] is Equipment)
            {
                Equipment eq = (Equipment)pinv.pIList[dragItem];
                if (putOver == eq.equipmentType.ToString())
                {
                    if (putOver == EquipmentType.Hat.ToString()) { hatImage.sprite = eq.icon; }
                    else if (putOver == EquipmentType.Glove.ToString()) { groveImage.sprite = eq.icon; }
                    else if (putOver == EquipmentType.Suit.ToString()) { suitImage.sprite = eq.icon; }
                    Debug.Log("Equiped:" + pinv.pIList[dragItem].names + "     AT " + putOver);
                }
            }
        }

        if (releaseDrag && dragItem != nowOver)
        {
            swapItem(dragItem, nowOver);
            releaseDrag = false;
        }
        releaseDrag = false;
    }

    //Player list zone
    public void setEmptySlot()
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

    public void swapItem(int swapfrom, int swapto)
    {
        Items temp = new BlankItem();
        temp = pinv.pIList[swapfrom];
        pinv.pIList[swapfrom] = pinv.pIList[swapto];
        pinv.pIList[swapto] = temp;

        changeItem(swapfrom);
        changeItem(swapto);
    }

    public void deleteItem(int deleteIndex)
    {
        pinv.pIList[deleteIndex] = new BlankItem();
        changeItem(deleteIndex);
    }

    public void changeItem(int index)
    {
        //Change item in that slot
        slotsList[index].GetComponent<SlotScript>().item = pinv.pIList[index];
        isMouseOver = false;
    }
}
