using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using System.Collections.Generic;

public static class SaveInventoryManager
{
    public static void SaveItemList(List<Item> itemList)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/inv.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        ItemListData data = new ItemListData(itemList);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static ItemListData LoadItemList()
    {
        string path = Application.persistentDataPath + "/inv.save";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            ItemListData data = formatter.Deserialize(stream) as ItemListData;
            stream.Close();
            AssignSprites(data); //Anyway I can avoid doing this? Moving sprites will break saves this way
            return data;
        }
        else
        {
            Debug.LogError($"Save file not found in {path}");
            return null;
        }
    }

    private static void AssignSprites(ItemListData data)
    {
        data.items.ForEach(i => {
            i.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(i.spriteLocation);
        });
    }
}
