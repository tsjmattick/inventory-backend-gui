using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryObject : ScriptableObject
{
    public string inventoryId;
    public string invName;
    public string description;
    public int maxSlots;
    public int availableSlots;
    public InvType inventoryType;
    public List<Collectible> items = new List<Collectible>();
}

public enum InvType
{
    Player,
    Tutorial,
    NPC,
    Default
}
