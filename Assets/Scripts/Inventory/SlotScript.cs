using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
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
		if (item != null) itemImage.sprite = Resources.Load<Sprite>(item.names);
	}

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        CharacterWindow.currentItem = item;
        CharacterWindow.nowOver = slotNumber;
        CharacterWindow.isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CharacterWindow.nowOver = CharacterWindow.dragItem;
        CharacterWindow.isMouseOver = false;
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
