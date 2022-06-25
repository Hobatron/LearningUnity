using System.Collections.Generic;
using UnityEditor;

[System.Serializable]
public class ItemListData
{
    public List<Item> items;

    public ItemListData(List<Item> itemSOList)
    {
        items = new List<Item>();
        itemSOList.ForEach(iSO => {
            items.Add(new Item {
                id = iSO.id,
                aquiredAt = iSO.aquiredAt,
                deletable = iSO.deletable,
                description = iSO.description,
                droppable = iSO.deletable,
                index = iSO.index,
                rarity = iSO.rarity,
                spriteLocation = AssetDatabase.GetAssetPath(iSO.sprite),
                stackable = iSO.stackable,
                type = iSO.type,
                value = iSO.value,
            });
        });
    }
}
