using UnityEngine;

namespace StarterAssets.InventoryUI
{
    public abstract class InventorySlotObject : MonoBehaviour
    {
        public int id;
        public string slotItemName;
        public string type;
        public string description;
        public bool isEmpty;
        public bool isAvailable;
        public Sprite icon;
        public bool isStackable;
        public int quantity;
    }
}