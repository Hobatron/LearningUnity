using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class inventory : MonoBehaviour
{
    [SerializeField] CanvasGroup canvas;
    [SerializeField] ItemListSO itemList;
    [SerializeField] GridLayoutGroup gridLayout;
    [SerializeField] VoidEventChannelSO toggleInventory;
    [SerializeField] RectTransform ItemsDir;
    [SerializeField] GameObject itemPrefab;
    private List<RectTransform> inventorySlots;
    private List<int> collisionDetector;

    IEnumerator Start()
    {
        inventorySlots = gridLayout.GetComponentsInChildren<RectTransform>()
                                    .Where(x => x.gameObject.transform != gridLayout.transform)
                                    .ToList();
        yield return null; //Hack to wait for Grid to draw
        itemList.GetItems().ForEach(InitItemInInventorySlot);
    }

    private void InitItemInInventorySlot(ItemSO item)
    {
        collisionDetector = new List<int>();
        if (collisionDetector.Contains(item.index))
        {
            if (GetLowestEmptySlot() != -1)
            {
                item.index = GetLowestEmptySlot();
            }
            else
            {
                //collision and full inv!
            }
        }
        collisionDetector.Add(item.index);
        // inventorySlots[item.index]
        Image itemImage = Instantiate(itemPrefab, 
                                        new Vector2 (inventorySlots[item.index].position.x, inventorySlots[item.index].position.y),
                                        Quaternion.identity, 
                                        ItemsDir).GetComponent<Image>();
        itemImage.sprite = item.sprite;
    }

    private void OnEnable() 
    {
        toggleInventory.OnEventRaised += ToggleInventroy;
    }

    private void OnDisable() 
    {
        toggleInventory.OnEventRaised -= ToggleInventroy;
    }

    private void ToggleInventroy()
    {
        if (canvas.interactable)
        {
            canvas.alpha = 0;
            canvas.interactable = false;
        }
        else
        {
            canvas.alpha = 1;
            canvas.interactable = true;
        }
    }

    private int GetLowestEmptySlot()
    {
        //TODO: Debug, clean, this code sucks. It's ugly and doesn't work
        collisionDetector.Sort();
        int i;
        for (i = 0; i < collisionDetector.Count; i++)
        {
            if (collisionDetector[i] != i)
            {
                return i;
            }
        }
        if (i !>= collisionDetector.Count)
        {
            return i;
        }
        return -1;
    }
}
