using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [System.Serializable]
    public class InventorySlot
    {
        public InventoryItem item;
        public int quantity;

        public bool IsEmpty => item == null || quantity <= 0;
    }

    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance { get; private set; }

        [SerializeField] private int _maxSlots = 20;
        public List<InventorySlot> Slots { get; private set; }

        public delegate void OnInventoryChanged();
        public event OnInventoryChanged InventoryChanged;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            Slots = new List<InventorySlot>(_maxSlots);
            for (int i = 0; i < _maxSlots; i++) Slots.Add(new InventorySlot());
        }

        public bool AddItem(InventoryItem item, int quantity = 1)
        {
            if (item == null || quantity <= 0) return false;

            int remaining = quantity;

            // 1️⃣ Fill existing stacks
            if (item.isStackable)
            {
                foreach (var slot in Slots)
                {
                    if (slot.item == item && slot.quantity < item.maxStack)
                    {
                        int space = item.maxStack - slot.quantity;
                        int toAdd = Mathf.Min(space, remaining);
                        slot.quantity += toAdd;
                        remaining -= toAdd;

                        if (remaining <= 0)
                        {
                            InventoryChanged?.Invoke();
                            return true; // all items added
                        }
                    }
                }
            }

            // 2️⃣ Fill empty slots
            foreach (var slot in Slots)
            {
                if (slot.IsEmpty)
                {
                    int toAdd = item.isStackable ? Mathf.Min(item.maxStack, remaining) : 1;
                    slot.item = item;
                    slot.quantity = toAdd;
                    remaining -= toAdd;

                    if (remaining <= 0)
                    {
                        InventoryChanged?.Invoke();
                        return true; // all items added
                    }
                }
            }

            // 3️⃣ If we still have items left, inventory is full
            if (remaining > 0)
            {
                Debug.LogWarning("Inventory full! Could not add " + remaining + " items.");
                InventoryChanged?.Invoke(); // update UI with partial add
                return false;
            }

            return true;
        }


        public void RemoveItem(InventorySlot slot)
        {
            slot.item = null;
            slot.quantity = 0;
            InventoryChanged?.Invoke();
        }

        public void NotifyInventoryChanged()
        {
            InventoryChanged?.Invoke();
        }
    }
}
