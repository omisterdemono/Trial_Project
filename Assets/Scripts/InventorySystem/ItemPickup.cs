using UnityEngine;

namespace InventorySystem
{
    public class ItemPickup : MonoBehaviour, IInteractable
    {
        [SerializeField] private InventoryItem _item;
        [SerializeField] private int _quantity = 1;

        private Transform _player;

        private void Start() => _player = GameObject.FindGameObjectWithTag("Player").transform;

        public void Interact()
        {
            if (Inventory.Instance.AddItem(_item, _quantity))
                Destroy(gameObject);
        }
    }
}