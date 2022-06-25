using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] ItemType allowedItemType;
    private int myIndex;

    private void Start()
    {
        myIndex = transform.GetSiblingIndex();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag  != null)
        {
            DragDrop itemTypeOfDrop = eventData.pointerDrag.GetComponent<DragDrop>();
            if (allowedItemType == ItemType.Any || allowedItemType == itemTypeOfDrop.itemType)
            {
                Vector2 newPos = GetComponent<RectTransform>().anchoredPosition;
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = newPos;
                itemTypeOfDrop.SetPos(myIndex);
            }
            else
            {
                itemTypeOfDrop.SetPos(-1);
            }
        }
        else
        {
            Debug.LogWarning("Missing gameObject!");
        }
    }
}
