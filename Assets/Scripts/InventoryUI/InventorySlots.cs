using System;
using System.Collections.Generic;
using System.Linq;
using StarterAssets.Inventories;
using StarterAssets.Tooltips;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static GameEvents;
using Image = UnityEngine.UI.Image;

namespace StarterAssets.InventoryUI
{
    public class InventorySlots:InventorySlotsObject
    {
        private int slotsCount; 
        private readonly Dictionary<InventorySlot, Collectible> _items = new Dictionary<InventorySlot, Collectible>();

        private void Start()
        {
            Current.OnItemPickupTriggerEnter += OnSlotUpdate;

            // we need to initialize each slot and set its available flag based on current 
            // inventory details
            maxSlots = currentInventory.maxSlots;
            availableSlots = currentInventory.availableSlots;
            slotsCount = 0;
            InitializeSlots();
        }

        private void InitializeSlots()
        {
            if (currentInventory == null) return;

            // first, set all slots available based on available player inventory size;
            foreach (var slot in slotsUI)
            {
                if (slotsCount < currentInventory.availableSlots)
                {
                    var slotDetails = slot.GetComponent<InventorySlot>();
                    UpdateCurrentInventorySlotDetails(slotDetails, slotsCount);
                }
                else
                {
                    slot.SetActive(false);
                }

                slotsCount++;
            }
        }
        
        /// <summary>
        /// this function checks itself against the players current inventory.  once verified, it updates current values based on the stored players
        /// inventory asset.  
        /// </summary>
        /// <param name="slotDetails">the extra details needed to setup each slot. </param>
        /// <param name="slotNum">the current slot number we are setting up</param>
        private void UpdateCurrentInventorySlotDetails(InventorySlot slotDetails, int slotNum)
        {
            if (slotNum < currentInventory.items.Count)
            {
                // update relevant details based on current inventory item information.  let the slot know if it is stackable so that we can increment items instead of adding items to new slot.
                // TODO ... get the last part working
                Debug.LogWarning($"Creating slot #{slotNum}");
                var inventoryItem = currentInventory.items[slotNum];
                slotDetails.id = slotNum;
                slotDetails.isAvailable = true;
                slotDetails.isEmpty = false;
                slotDetails.isStackable = inventoryItem.stackable;
                slotDetails.slotItemName = inventoryItem.itemKey;
                slotDetails.quantity += inventoryItem.amount;
                slotDetails.type = inventoryItem.itemType.ToString();
                slotDetails.icon = inventoryItem.displayIcon;
                // this updates the dictionary that holds the internal items list.
                _items.Add(slotDetails,inventoryItem);
                // finally, update the current inventory slot GUI image and text values

                var slot = slotsUI.ToList().FirstOrDefault(s => s.GetComponent<InventorySlot>().isAvailable == true && 
                                                                s.GetComponent<InventorySlot>().isEmpty == false);
                                                                
                if (slot != null)
                {
                    slot.gameObject.GetComponent<Image>().overrideSprite = slotDetails.icon;
                    slot.gameObject.GetComponent<TooltipTrigger>().header = slotDetails.name;
                    slot.gameObject.GetComponent<TooltipTrigger>().content = slotDetails.description;
                    slot.GetComponentInChildren<TextMeshProUGUI>().text = slotDetails.quantity.ToString();
                }
            }
            else
            {
                slotDetails.id = slotNum;
                slotDetails.isAvailable = true;
                slotDetails.isEmpty = true;
            }


        }

        private void UpdateCurrentInventorySlotGUIDisplay(InventorySlot slotDetails)
        {
            var slot = slotsUI.ToList().SingleOrDefault(s => s.GetComponent<InventorySlot>().id == slotDetails.id);
            if (slot == null) return;
            
        }


        protected override void OnSlotUpdate(Collectible collectible, int qty)
        {
            // get the first empty slot, then update slots image, type, desc, and quantity
            try
            {
                // check the slots for similar items.  if we have one, simply update the quantity.  check that it is not maxed out.  
                // not sure of what that ceiling might be yet, i am guessing certain types would only stack so high.

                var slot = slotsUI.ToList()
                    .SingleOrDefault(s => s.GetComponent<InventorySlot>().slotItemName == collectible.itemKey);

                if (slot)
                {
                    slot.GetComponent<InventorySlot>().quantity += qty;
                    slot.GetComponentInChildren<TextMeshProUGUI>().text = slot.GetComponent<InventorySlot>().quantity.ToString();
                    
                    // we need to update current inventory values here.

                    //TODO Bug... the player current inventory is updating, however, its updating the collectibles values.  instead, the slots
                    //info needs to store its data in the items dictionary as it is a copy of player current inventory.  once this is setup, then the connection to the
                    //UI should work.  maybe 4.21.22 JAM
                    
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
        
        public override bool AddItem(Collectible collectible, int qty)
        {
            // this is for creating a new slot...
            var slot = slotsUI.ToList().FirstOrDefault(s => s.GetComponent<InventorySlot>().isAvailable == true 
                                                            && s.GetComponent<InventorySlot>().isEmpty == true
                                                            && s.GetComponent<InventorySlot>().isStackable == true);
            if (slot == null) return false;
            
            var slotDetails = slot.GetComponent<InventorySlot>();
            Debug.LogWarning($"item picked up, updating slot {slotDetails.id}");
            slotDetails.slotItemName = collectible.itemKey;
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
            slot.GetComponentInChildren<TextMeshProUGUI>().text = slotDetails.quantity.ToString();

            return true;
        }

        public override bool RemoveItem()
        {
            // this is what happens when we click the button...
            return isSlotUpdated;
        }
    }
}