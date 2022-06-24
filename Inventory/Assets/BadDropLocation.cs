using UnityEngine;
using UnityEngine.EventSystems;

public class BadDropLocation : MonoBehaviour, IDropHandler
{
    [SerializeField] BoolEventChannelSO canDrop;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag  != null)
        {
            canDrop.RaiseEvent(false);
        }
        else
        {
            Debug.LogWarning("Missing gameObject!");
        }
    }
}
