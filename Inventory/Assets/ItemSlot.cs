using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] AllowedItemTypeSO allowedItemType;
    [SerializeField] BoolEventChannelSO canDrop;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag  != null)
        {
            AllowedItemTypeSO itemTypeOfDrop = eventData.pointerDrag.GetComponent<DragDrop>().itemTypeSO;
            if (allowedItemType.itemType == ItemType.Any || allowedItemType == itemTypeOfDrop)
            {
                Vector2 newPos = GetComponent<RectTransform>().anchoredPosition;
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = newPos;
                canDrop.RaiseEvent(true);
            }
            else
            {
                canDrop.RaiseEvent(false);
            }
        }
        else
        {
            Debug.LogWarning("Missing gameObject!");
        }
    }
}
