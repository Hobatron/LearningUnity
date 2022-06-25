using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour,IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
{
    [SerializeField] public ItemType itemType;
    [SerializeField] IconItem myItem;
    private RectTransform rectTransform;
    private Vector2 oldPos;
    private CanvasGroup canvasGroup;

    private void Start() 
    {
        oldPos = rectTransform.anchoredPosition;
    }
    public void SetPos(int dropIndex)
    {
        //TODO: Is this an okay solution? (gets called on every item every time an item is moved)
        if (dropIndex >= 0)
        {
            oldPos = rectTransform.anchoredPosition;
            myItem.item.index = dropIndex;
        }
        else
        {
            rectTransform.anchoredPosition = oldPos; //Bad drop
        }
    }

    private void Awake() 
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        oldPos = GetComponent<RectTransform>().anchoredPosition;
    }
}
