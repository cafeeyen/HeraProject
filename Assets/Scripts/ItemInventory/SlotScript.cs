using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SlotScript : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler{
    
	public Items item;
	Image itemImage;
	public int slotNumber;


    // Use this for initialization
	void Start () {
		itemImage = gameObject.transform.GetChild(0).GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if(item != null)
		{
			itemImage.enabled = true;
			itemImage.name = "image" + slotNumber;
			itemImage.sprite = item.icon;
		}
		else
		{
			itemImage.enabled = false;	
		}
	}


	void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if(item != null)
		{
			Debug.Log(item.names + " " + itemImage.name);
			
		}
    }
	
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
		CharacterWindow.isMouseOver = true;
		CharacterWindow.currentItem = item;
		CharacterWindow.nowOver = slotNumber;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
		CharacterWindow.isMouseOver = false;
		CharacterWindow.nowOver = CharacterWindow.dragItem;
    }

	public void OnPointerDown(PointerEventData eventData)
    {
		CharacterWindow.dragItem = slotNumber;
		CharacterWindow.dragging = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
		CharacterWindow.releaseDrag = true;
		CharacterWindow.dragging = false;
    }
}
