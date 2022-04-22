using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Collectible : ScriptableObject
{
    public string itemKey;
    [TextArea(10, 20)] 
    public string description;
    public int amount;
    public bool stackable;
    public Sprite displayIcon;
    public ItemType itemType;
    public int buyPrice;
    public int sellPrice;
    public BarterValue barterValue;
}

public enum BarterValue
{
    Great,
    None,
    Junk,
    Average
}

public enum ItemType
{
    Food,
    Tool,
    Material
}
public enum Type
{
    Crafting,
    QuestItem,
    Sellable
}

public enum Quality
{
    Broken,
    Disgusting,
    Good,
    High,
    Pristine
}

public enum QualityType
{
    Common,
    Rare,
    Legendary,
    Unique
}