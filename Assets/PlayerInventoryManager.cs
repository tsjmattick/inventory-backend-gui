using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using StarterAssets.Inventories;
using StarterAssets.InventoryUI;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    void Start()
    {
        GameEvents.Current.OnItemPickupTriggerEnter += OnItemPickup;
    }

    private void OnItemPickup(Collectible collectible, int qty)
    {
        
        
        // we need to check the item keys to see if we already have the collectible.  it should be the same as the slots name;
        // we either have already collected this or we have not.  this adds item to 
        // dictionary or public list, and finishes by updating the value.
        // if(!_items.ContainsKey(.itemKey))
        // {
        //     currentInventory.items.Add(collectible);
        //     _items.Add(collectible.itemKey, qty);
        // }
        // else
        // {
        //     _items[collectible.itemKey]+= qty;
        // }
    }
    private void OnDestroy()
    {
        GameEvents.Current.OnItemPickupTriggerEnter -= OnItemPickup;
    }
}
