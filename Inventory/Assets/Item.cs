using System;
using UnityEngine;
#nullable enable
public interface Item
{
    [SerializeField] public string id {get; set;}
    [SerializeField] public string description {get; set;}
    [SerializeField] public DateTime aquiredAt {get; set;}
    [SerializeField] public float value {get; set;}
    [SerializeField] public Rarity rarity {get; set;}
    [SerializeField] public ItemType type {get; set;}
    [SerializeField] public Sprite sprite {get; set;}
    [SerializeField] public bool stackable {get; set;}
    [SerializeField] public bool droppable {get; set;}
    [SerializeField] public bool deletable {get; set;}
}
