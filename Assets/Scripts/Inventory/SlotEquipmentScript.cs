using UnityEngine;
using UnityEngine.EventSystems;

public class SlotEquipmentScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    public EquipmentType slotFor;
    public Items item;
    public string slotName = "";

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (slotFor == EquipmentType.None) CharacterWindow.putOver = slotName;
        else CharacterWindow.putOver = slotFor.ToString();

        CharacterWindow.isMouseOver = true;
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
