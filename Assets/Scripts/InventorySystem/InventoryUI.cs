using UnityEngine;

namespace InventorySystem.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private GameObject _inventoryPanel;
        [SerializeField] private Transform _slotsParent;
        [SerializeField] private InventorySlotUI _slotPrefab;

        private Inventory _inventory;

        private void Start()
        {
            _inventory = Inventory.Instance;
            _inventory.InventoryChanged += RefreshUI;

            // Create slots
            foreach (var slot in _inventory.Slots)
                Instantiate(_slotPrefab, _slotsParent).Setup(slot);

            RefreshUI();
        }

        private void RefreshUI()
        {
            var slotUIs = _slotsParent.GetComponentsInChildren<InventorySlotUI>();
            for (int i = 0; i < _inventory.Slots.Count; i++)
                slotUIs[i].UpdateSlot(_inventory.Slots[i]);
        }
    }
}
