using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SlotEquipmentScript : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler{
    
	public EquipmentType slotFor;
	public Items item;
	public string slotName = "";
	//public int slotNumber;


    // Use this for initialization
	void Start () {
		//itemImage = gameObject.transform.GetChild(0).GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		// if(item != null)
		// {
		// 	itemImage.enabled = true;
		// 	itemImage.name = "image" + slotNumber;
		// 	itemImage.sprite = item.icon;
		// }
		// else
		// {
		// 	itemImage.enabled = false;	
		// }
	}


	void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
    }
	
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
		Debug.Log("overSP" + " " + slotFor.ToString());
		CharacterWindow.isMouseOver = true;
		if(slotFor == EquipmentType.None){CharacterWindow.putOver = slotName;}
		else{CharacterWindow.putOver = slotFor.ToString();}
		CharacterWindow.overSpecialSlot = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
		Debug.Log("resetSP" + " " + slotFor.ToString());
		CharacterWindow.isMouseOver = false;
		CharacterWindow.overSpecialSlot = false;
    }

	public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("dragItemSP" + " " +slotFor.ToString());
		CharacterWindow.dragging = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
		CharacterWindow.releaseDrag = true;
		CharacterWindow.dragging = false;
        Debug.Log("releaseDragSP" + " " +slotFor.ToString());
    }
}
