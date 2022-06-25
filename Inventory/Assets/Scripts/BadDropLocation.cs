using UnityEngine;
using UnityEngine.EventSystems;

public class BadDropLocation : MonoBehaviour, IDropHandler
{
    //summery
    //Attach this to any places inside the canvas that items shouldn't drop
    //i.e. the main panel uses this to stop items from just dropping anywhere
    //summery
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag  != null)
        {
            eventData.pointerDrag.GetComponent<DragDrop>().SetPos(-1);
        }
        else
        {
            Debug.LogWarning("Missing gameObject!");
        }
    }
}
