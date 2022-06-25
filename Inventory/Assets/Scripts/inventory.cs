using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class inventory : MonoBehaviour
{
    [SerializeField] CanvasGroup canvas;
    [SerializeField] GridLayoutGroup gridLayout;
    [SerializeField] VoidEventChannelSO toggleInventory;
    [SerializeField] RectTransform itemsDir;
    [SerializeField] DataEventChannelSO dataEvent;
    [SerializeField] GameObject itemPrefab;
    private List<RectTransform> inventorySlots;
    private List<int> collisionDetector = new List<int>();
    private List<GameObject> itemPrefabList = new List<GameObject>();
    
    [SerializeField] List<Item> itemList = new List<Item>(); //End of list so you can edit in inspector??

    IEnumerator Start()
    {
        inventorySlots = gridLayout.GetComponentsInChildren<RectTransform>()
                                    .Where(x => x.gameObject.transform != gridLayout.transform)
                                    .ToList();
        yield return null; //Hack to wait for Grid to draw
        itemList.ForEach(CreateItemGameObject);
    }

    private void CreateItemGameObject(Item item)
    {
        if (CollisionDetected(item))
        {
            //Send Alert, can't pick up
        }
        else
        {
            GameObject gameObject = Instantiate(itemPrefab, 
                        inventorySlots[item.index].position,
                        Quaternion.identity, 
                        itemsDir);
            gameObject.GetComponent<Image>().sprite = item.sprite;
            gameObject.GetComponent<IconItem>().item = item;
            itemPrefabList.Add(gameObject);
        }
    }

    private void OnEnable() 
    {
        toggleInventory.OnEventRaised += ToggleInventroy;
        dataEvent.SaveEventRaised += SaveInventory;
        dataEvent.LoadEventRaised += LoadInventory;
    }

    private void OnDisable() 
    {
        toggleInventory.OnEventRaised -= ToggleInventroy;
        dataEvent.SaveEventRaised -= SaveInventory;
        dataEvent.LoadEventRaised -= LoadInventory;
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

    private void SaveInventory()
    {
        
        SaveInventoryManager.SaveItemList(itemList);
    }

    private void LoadInventory()
    {
        ClearAllSlots();
        itemList = SaveInventoryManager.LoadItemList().items;
        itemList.ForEach(CreateItemGameObject);
    }

    private void ClearAllSlots()
    {
        itemPrefabList.ForEach(g => Destroy(g));
        collisionDetector = new List<int>();
        itemPrefabList = new List<GameObject>();
    }

    private bool CollisionDetected(Item item)
    {
        if (collisionDetector.Contains(item.index))
        {
            int lowestIndex = GetLowestEmptySlot();
            if (lowestIndex != -1)
            {
                item.index = lowestIndex;
            }
            else
            {
                return true; //collision and no free space, don't allow item to add
            }
        }
        collisionDetector.Add(item.index);
        return false; //no Collision, free to add
    }

    private int GetLowestEmptySlot()
    {
        collisionDetector.Sort();
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            try
            {
                if (collisionDetector[i] != i)
                {
                    return i;
                };
            }
            catch (System.Exception)
            {
                return i;
            }
        }
        return -1;
    }
}
