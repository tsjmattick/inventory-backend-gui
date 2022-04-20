using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static GameEvents;
using Image = UnityEngine.UI.Image;

namespace StarterAssets.InventoryUI
{
    public class InventorySlots:InventorySlotsObject
    {
        private int slotsCount = 1;
        
        private void Start()
        {
            Current.OnItemPickupTriggerEnter += OnInventorySlotsSlotUpdate;

            // we need to initialize each slot and set its available flag based on current 
            // inventory details
            maxSlots = currentInventory.maxSlots;
            availableSlots = currentInventory.availableSlots;
            
            // TODO fill slots with player current inventory items
            
            foreach (var slot in slotsUI)
            {
                
                // set all slots id;
                var slotDetails = slot.GetComponent<InventorySlot>();
                slotDetails.isEmpty = slotDetails.quantity == 0 ? true : false;
                slotDetails.id = slotsCount;
                slotDetails.isStackable = true;
                // check to see if slot is available, otherwise disable it in UI.
                if (slotsCount <= currentInventory.availableSlots)
                {
                    slotDetails.isStackable = true;
                    slotDetails.isAvailable = true;
                    
                }
                else
                {
                    slot.SetActive(false);
                }

                slotsCount++;
            }
        }

        
        
        public override bool AddItem()
        {
            return isSlotUpdated;
        }

        public override bool RemoveItem()
        {
            return isSlotUpdated;
        }

        protected override void OnInventorySlotsSlotUpdate(Collectible collectible, int qty)
        {
            // get the first empty slot, then update slots image, type, desc, and quantity
            try
            {
                var slot = slotsUI.ToList().FirstOrDefault(s => s.GetComponent<InventorySlot>().isAvailable == true 
                                                                && s.GetComponent<InventorySlot>().isEmpty == true
                                                                    && s.GetComponent<InventorySlot>().isStackable == true);
                if (slot != null)
                {
                    var slotDetails = slot.GetComponent<InventorySlot>();
                    Debug.LogWarning($"item picked up, updating slot {slotDetails.id}");
                    slotDetails.slotItemName = collectible.name;
                    slotDetails.icon = collectible.displayIcon;
                    slotDetails.description = collectible.description;
                    slotDetails.type = collectible.itemType.ToString();
                    slotDetails.quantity = qty;
                    slotDetails.isEmpty = false;
                    slotDetails.isAvailable = false;
                    slotDetails.isStackable = true;
                    
                    // update the visible icon...
                    slot.gameObject.GetComponent<Image>().overrideSprite = slotDetails.icon;
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Unable to update inventory Image, {e.Message}");
            }

        }

        // private void Update()
        // {
        //     throw new NotImplementedException();
        // }
    }
}