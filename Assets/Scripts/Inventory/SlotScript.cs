using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SlotScript : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    
	public Items item;
	public int slotNumber;

    Image itemImage;

    void Start ()
    {
		itemImage = gameObject.transform.GetChild(0).GetComponent<Image>();
	}

	void Update ()
    {
		if(item != null)
		{
			itemImage.name = "image" + slotNumber;
			itemImage.sprite = Resources.Load<Sprite>(item.names);
		}
	}


	void IPointerClickHandler.OnPointerClick(PointerEventData eventData) { }
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
