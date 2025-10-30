using UnityEngine;

namespace InventorySystem
{
    public enum ItemType { Potion, Weapon, Armor, Misc }

    [CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
    public class InventoryItem : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
        public ItemType itemType;
        public bool isStackable;
        public int maxStack = 99;
        public string description;
        public bool isUsable;

        public virtual void Use()
        {
            Debug.Log($"Used {itemName}");
        }
    }
}