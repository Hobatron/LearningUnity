using System;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string id;
    public string description;
    public DateTime aquiredAt;
    public Rarity rarity;
    public ItemType type;
    public float value;
    public int index;
    public string spriteLocation; //hacky work around to get sprite on load
    [NonSerializedAttribute] public Sprite sprite;
    public bool stackable;
    public bool droppable;
    public bool deletable;
}
