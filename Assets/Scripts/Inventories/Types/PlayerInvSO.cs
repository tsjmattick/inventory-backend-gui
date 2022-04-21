using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets.Inventories;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventories/Inventory")]
public class PlayerInvSO : Inventory
{
    public void OnValidate()
    {
        invName = this.name;
    }
    public void OnEnable()
    {
        inventoryType = InvType.Default;
        invName = this.name;
        inventoryId =
            $"inv_{inventoryType}_{invName}_{Guid.NewGuid().ToString().Substring(Guid.NewGuid().ToString().Length - 12)}";
    }
}
