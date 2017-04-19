using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SlotEquipmentScript : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    public EquipmentType slotFor;
    public Items item;
    public string slotName = "";

    void Start()
    {
        item = new BlankItem();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        CharacterWindow.isMouseOver = true;
        if (slotFor == EquipmentType.None) CharacterWindow.putOver = slotName;
        else CharacterWindow.putOver = slotFor.ToString();
        CharacterWindow.currentItem = item;
        CharacterWindow.overSpecialSlot = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CharacterWindow.isMouseOver = false;
        CharacterWindow.overSpecialSlot = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CharacterWindow.dragging = true;
        CharacterWindow.spDragItem = item;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        CharacterWindow.releaseDrag = true;
        CharacterWindow.dragging = false;
    }
}
