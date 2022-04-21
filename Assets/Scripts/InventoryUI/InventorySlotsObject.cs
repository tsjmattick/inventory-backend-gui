using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace StarterAssets.InventoryUI
{
    public abstract class InventorySlotsObject : MonoBehaviour
    {
        public Inventory currentInventory;
        public GameObject[] slotsUI;
        public int availableSlots;
        public int maxSlots;
        public bool isSlotUpdated = false;
        public abstract bool AddItem(Collectible collectible, int qty);
        public abstract bool RemoveItem();

        protected abstract void OnSlotUpdate(Collectible collectible, int qty);

    }
}