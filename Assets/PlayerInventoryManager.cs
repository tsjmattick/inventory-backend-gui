using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using StarterAssets.Inventories;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    public Inventory currentInventory;
    private readonly Dictionary<Collectible, int> _items = new Dictionary<Collectible, int>();
    void Start()
    {
        GameEvents.Current.OnItemPickupTriggerEnter += OnItemPickup;
        CreateItemsList();
    }
    private void CreateItemsList()
    {
        if (currentInventory == null) return;
        foreach (var item in currentInventory.items)
        {
            _items.Add(item, item.amount);
        }
    }
    private void OnItemPickup(Collectible collectible, int qty)
    {
        // we either have already collected this or we have not.  this adds item to 
        // dictionary or public list, and finishes by updating the value.
        if(!_items.ContainsKey(collectible))
        {
            currentInventory.items.Add(collectible);
            _items.Add(collectible, qty);
        }
        else
        {
            _items[collectible]+= qty;
        }
    }
    private void OnDestroy()
    {
        GameEvents.Current.OnItemPickupTriggerEnter -= OnItemPickup;
    }
}
