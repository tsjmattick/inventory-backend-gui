using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
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
        // public Dictionary<InventorySlot, int> CurrentInventorySlots;
        // public abstract InventorySlot AddSlot(InventorySlot inventorySlot, int quantity );
        // public abstract bool UpdateSlot(InventorySlot inventorySlot, int quantity);
        // public abstract bool SetAvailableSlots();
        // public abstract bool RemoveSlot(InventorySlot slot);

        public abstract bool AddItem();
        public abstract bool RemoveItem();

        protected abstract void OnInventorySlotsSlotUpdate(Collectible collectible, int qty);

    }
}