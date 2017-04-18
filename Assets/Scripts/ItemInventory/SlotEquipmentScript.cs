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

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("overSP" + " " + slotFor.ToString());
        CharacterWindow.isMouseOver = true;
        if (slotFor == EquipmentType.None) CharacterWindow.putOver = slotName;
        else CharacterWindow.putOver = slotFor.ToString();
        CharacterWindow.overSpecialSlot = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("resetSP" + " " + slotFor.ToString());
        CharacterWindow.isMouseOver = false;
        CharacterWindow.overSpecialSlot = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("dragItemSP" + " " +slotFor.ToString());
        CharacterWindow.dragging = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        CharacterWindow.releaseDrag = true;
        CharacterWindow.dragging = false;
        //Debug.Log("releaseDragSP" + " " +slotFor.ToString());
    }
}
