using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    public Inventory currentInventory;

    public Dictionary<Collectible, int> Items = new Dictionary<Collectible, int>();
    void Start()
    {
        GameEvents.Current.OnItemPickupTriggerEnter += OnItemPickup;
    }
    private void OnItemPickup(Collectible collectible, int qty)
    {
        // we either have already collected this or we have not.  this adds item to 
        // dictionary or public list, and finishes by updating the value.
        if(!Items.ContainsKey(collectible))
        {
            currentInventory.items.Add(collectible);
            Items.Add(collectible, qty);
        }
        else
        {
            Items[collectible] += qty;
        }
    }
    
    
    private void OnDestroy()
    {
        GameEvents.Current.OnItemPickupTriggerEnter -= OnItemPickup;
    }
}
