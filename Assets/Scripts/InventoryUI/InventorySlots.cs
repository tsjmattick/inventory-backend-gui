using System;
using System.Linq;
using StarterAssets.Tooltips;
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
            Current.OnItemPickupTriggerEnter += OnSlotUpdate;
            
            // we need to initialize each slot and set its available flag based on current 
            // inventory details
            maxSlots = currentInventory.maxSlots;
            availableSlots = currentInventory.availableSlots;
            
            // TODO fill slots with player current inventory items -- Do this next
            
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

        public override bool AddItem(Collectible collectible, int qty)
        {
            // this is for creating a new slot...
            var slot = slotsUI.ToList().FirstOrDefault(s => s.GetComponent<InventorySlot>().isAvailable == true 
                                                            && s.GetComponent<InventorySlot>().isEmpty == true
                                                            && s.GetComponent<InventorySlot>().isStackable == true);
            if (slot == null) return false;
            
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
            slot.gameObject.GetComponent<TooltipTrigger>().header = slotDetails.name;
            slot.gameObject.GetComponent<TooltipTrigger>().content = slotDetails.description;

            return true;
        }

        public override bool RemoveItem()
        {
            return isSlotUpdated;
        }

        protected override void OnSlotUpdate(Collectible collectible, int qty)
        {
            // get the first empty slot, then update slots image, type, desc, and quantity
            try
            {
                // check the slots for similar items.  if we have one, simply update the quty.  check that it is not maxxed out.  
                // not sure of what that ceiling might be yet, i am guessing certain types would only stack so high.

                var slot = slotsUI.ToList()
                    .SingleOrDefault(s => s.GetComponent<InventorySlot>().slotItemName == collectible.name);

                if (slot)
                {
                    slot.GetComponent<InventorySlot>().quantity += qty;
                }
                else
                {
                    if (!AddItem(collectible, qty))
                    {                
                        Debug.LogWarning($"Unable to add item, inventory seems full. {collectible.name}");

                        // we need to let the player know inventory is full;
                    };
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Unable to update inventory Image, {e.Message}");
            }
        }
    }
}