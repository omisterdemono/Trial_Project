using UnityEngine;

namespace InventorySystem
{
    public class ItemPickup : MonoBehaviour, IInteractable
    {
        [SerializeField] private InventoryItem _item;
        [SerializeField] private int _quantity = 1;

        public void Interact()
        {
            if (Inventory.Instance.AddItem(_item, _quantity))
                Destroy(gameObject);
        }
    }
}