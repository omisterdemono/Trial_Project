using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace InventorySystem.UI
{
    public class InventorySlotUI : MonoBehaviour,
        IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler,
        IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _quantityText;
        private InventorySlot _slot;

        public void Setup(InventorySlot slot) => _slot = slot;

        public void UpdateSlot(InventorySlot slot)
        {
            _slot = slot;
            bool hasItem = !_slot.IsEmpty;
            _icon.enabled = hasItem;
            _quantityText.enabled = hasItem && _slot.item.isStackable;
            if (hasItem)
            {
                _icon.sprite = _slot.item.icon;
                _quantityText.text = _slot.quantity.ToString();
            }
        }

        // Drag-drop
        private static InventorySlotUI _draggingSlot;
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_slot.IsEmpty) return;
            _draggingSlot = this;
        }

        public void OnDrag(PointerEventData eventData) { }
        public void OnEndDrag(PointerEventData eventData) => _draggingSlot = null;

        public void OnDrop(PointerEventData eventData)
        {
            if (_draggingSlot == null || _draggingSlot == this) return;

            // Swap slots
            var temp = _slot.item;
            var tempQty = _slot.quantity;
            _slot.item = _draggingSlot._slot.item;
            _slot.quantity = _draggingSlot._slot.quantity;
            _draggingSlot._slot.item = temp;
            _draggingSlot._slot.quantity = tempQty;

            Inventory.Instance.NotifyInventoryChanged();
        }

        // Tooltip
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_slot.IsEmpty)
                ItemTooltipUI.Instance.Show(_slot.item);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ItemTooltipUI.Instance.Hide();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_slot == null || _slot.IsEmpty || !_slot.item.isUsable) return;

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                _slot.item.Use(); // Call the item's Use() method

                // Decrease quantity if stackable
                if (_slot.item.isStackable)
                {
                    _slot.quantity--;
                    if (_slot.quantity <= 0)
                    {
                        Inventory.Instance.RemoveItem(_slot);
                    } 
                }
                else
                {
                    Inventory.Instance.RemoveItem(_slot);
                }

                Inventory.Instance.NotifyInventoryChanged(); // refresh UI
            }
        }
    }
}
