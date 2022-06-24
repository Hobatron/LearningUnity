using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item List")]
public class ItemListSO : ScriptableObject
{
    [SerializeField] List<ItemSO> itemList;

    public void addItem(ItemSO item)
    {
        itemList.Add(item);
    }

    public void RemoveItem(ItemSO item)
    {
        itemList.Remove(item);
    }

    public void RemoveById(string id)
    {
        itemList.Remove(itemList.Find(item => item.id == id));
    }

    public ItemSO GetItemById(string id)
    {
        return itemList.Find(item => item.id == id);
    }

    public bool HasItemWithID(string id)
    {
        return itemList.Exists(item => item.id == id);;
    }

    public List<ItemSO> GetItems()
    {
        return itemList;
    }
}
