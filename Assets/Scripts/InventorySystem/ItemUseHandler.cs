using UnityEngine;

namespace InventorySystem
{
    public class ItemUseHandler : MonoBehaviour
    {
        private Inventory _inventory;

        private void Start() => _inventory = Inventory.Instance;

        public void UseItem(InventorySlot slot)
        {
            if (slot.item == null) return;

            slot.item.Use();
            if (slot.item.isStackable)
            {
                slot.quantity--;
                if (slot.quantity <= 0)
                    _inventory.RemoveItem(slot);
            }
            else
            {
                _inventory.RemoveItem(slot);
            }
        }
    }
}