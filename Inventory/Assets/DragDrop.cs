using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour,IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
{
    [SerializeField] public AllowedItemTypeSO itemTypeSO;
    private RectTransform rectTransform;
    private Vector2 oldPos;
    private CanvasGroup canvasGroup;
    
    private void OnEnable() 
    {
        itemTypeSO.CanDrop.OnEventRaised += ResetPosOnFail;
    }

    private void OnDisable() 
    {
        itemTypeSO.CanDrop.OnEventRaised -= ResetPosOnFail;
    }

    private void ResetPosOnFail(bool canDrop)
    {
        if (!canDrop)
        {
            rectTransform.anchoredPosition = oldPos;
        }
        else
        {
            oldPos = rectTransform.anchoredPosition;
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
