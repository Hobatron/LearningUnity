using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] public float armorValue;
    [SerializeField] public float movementSpeedPenalty;
    [SerializeField] public float value;
    [SerializeField] public int index;
    [SerializeField] public int sockets;
    [SerializeField] public int levelRequirment;
    [SerializeField] public string id;
    [SerializeField] public string description;
    [SerializeField] public DateTime aquiredAt;
    [SerializeField] public Rarity rarity;
    [SerializeField] public ItemType type;
    [SerializeField] public Sprite sprite;
    [SerializeField] public bool stackable;
    [SerializeField] public bool droppable;
    [SerializeField] public bool deletable;
}
