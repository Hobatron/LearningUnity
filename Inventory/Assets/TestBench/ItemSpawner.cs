using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    int idInc = 0;
    private List<ItemType> itemsIHaveSpritesFor = new List<ItemType>{
        ItemType.Armor,
        ItemType.Weapon,
        ItemType.Hand,
        ItemType.Helmet,
        ItemType.Feet
    };
    
    #region  sprit file names
    private string[] armorNames = new string[] {
        "Armor_1",
        "Armor_2",
        "Armor_3",
        "Armor_4",
        "Armor_5",
        "Armor_6",
        "Armor_7",
        "Armor_8",
        "Armor_9"
    };

    private string[] weaponNames = new string[] {
        "Axe",
        "Blase",
        "Bow",
        "Claw",
        "Knife",
        "Mace",
        "Spear",
        "Staff",
        "Sword"
    };

    private string[] gloveNames = new string[] {
        "Gloves_1",
        "Gloves_2",
        "Gloves_3",
        "Gloves_4",
        "Gloves_5",
        "Gloves_6",
        "Gloves_7",
        "Gloves_8",
        "Gloves_9"
    };

    private string[] headNames = new string[] {
        "Hat_1",
        "Hat_2",
        "Hat_3",
        "Hat_4",
        "Hat_5",
        "Hat_6",
        "Hat_7",
        "Hat_8",
        "Hat_9"
    };

    private string[] shoesNames = new string[] {
        "Shoes_1",
        "Shoes_2",
        "Shoes_3",
        "Shoes_4",
        "Shoes_5",
        "Shoes_6",
        "Shoes_7",
        "Shoes_8",
        "Shoes_9"
    };
    #endregion

    public void OnSpawnItem()
    {
        Item item = MakeRandomItem();
        
    }

    private Item MakeRandomItem()
    {
        idInc++;
        ItemType itemType = SelectItemType();
        Rarity rarity = RandomEnumValue<Rarity>();
        return new Item {
            id = $"RandomItem{idInc}",
            aquiredAt = DateTime.Now,
            deletable = true,
            description = "Your favorite item",
            droppable = true,
            index = 0,
            rarity = rarity,
            sprite = RandomSprite(itemType),
            stackable = false,
            type = itemType,
            value = 30 * (int)rarity
        };
    }

    private Sprite RandomSprite(ItemType itemType)
    {
        string spriteFileName = "VIASS Icon Style/";
        switch (itemType)
        {
            case ItemType.Armor:
                spriteFileName += armorNames[UnityEngine.Random.Range(0, armorNames.Length)];
                break;
            case ItemType.Weapon:
                spriteFileName += weaponNames[UnityEngine.Random.Range(0, weaponNames.Length)];
                break;
            case ItemType.Hand:
                spriteFileName += gloveNames[UnityEngine.Random.Range(0, gloveNames.Length)];
                break;
            case ItemType.Helmet:
                spriteFileName += headNames[UnityEngine.Random.Range(0, headNames.Length)];
                break;
            case ItemType.Feet:
                spriteFileName += shoesNames[UnityEngine.Random.Range(0, shoesNames.Length)];
                break;
            default:
                break;
        }
        return AssetDatabase.LoadAssetAtPath<Sprite>(spriteFileName);
    }

    private ItemType SelectItemType()
    {
        ItemType itemType = ItemType.None;
        do
        {
            itemType = RandomEnumValue<ItemType>();
        } while (!itemsIHaveSpritesFor.Contains(itemType));
        return itemType;
    }

    static T RandomEnumValue<T> ()
    {
        var v = Enum.GetValues (typeof (T));
        return (T) v.GetValue (UnityEngine.Random.Range(0,v.Length));
    }
}
