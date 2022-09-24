using System;
using UnityEngine;

namespace Spotnose.Stardust
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private int _metal;

        public int GetItemCount(InventoryItemType inventoryItemType)
        {
            return inventoryItemType switch
            {
                InventoryItemType.Metal => _metal,
                _ => 0
            };
        }
        
        public void SetItemCount(InventoryItemType inventoryItemType, int count)
        {
            switch (inventoryItemType)
            {
                case InventoryItemType.Metal:
                    _metal = count;
                    Events.OnMetalCountChanged.Invoke(_metal);
                    break;
            }
        }

        public void AddItem(InventoryItemType inventoryItemType, int amount)
        {
            switch (inventoryItemType)
            {
                case InventoryItemType.Metal:
                    _metal += amount;
                    Events.OnMetalCountChanged.Invoke(_metal);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(inventoryItemType), inventoryItemType, null);
            }
        }
        
        public void RemoveItem(InventoryItemType inventoryItemType, int amount)
        {
            switch (inventoryItemType)
            {
                case InventoryItemType.Metal:
                    _metal = Mathf.Max(0, _metal - amount);
                    Events.OnMetalCountChanged.Invoke(_metal);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(inventoryItemType), inventoryItemType, null);
            }
        }
        
#if UNITY_EDITOR
        
        [ContextMenu("Add 5 Metal")]
        private void Add5Metal() => AddItem(InventoryItemType.Metal, 5);
        
        [ContextMenu("Remove 5 Metal")]
        private void Remove5Metal() => RemoveItem(InventoryItemType.Metal, 5);
        
        [ContextMenu("Add 10 Metal")]
        private void Add10Metal() => AddItem(InventoryItemType.Metal, 10);
        
        [ContextMenu("Remove 10 Metal")]
        private void Remove10Metal() => RemoveItem(InventoryItemType.Metal, 10);
        
        [ContextMenu("Add 20 Metal")]
        private void Add20Metal() => AddItem(InventoryItemType.Metal, 20);
        
        [ContextMenu("Remove 20 Metal")]
        private void Remove20Metal() => RemoveItem(InventoryItemType.Metal, 20);
        
        [ContextMenu("Add 50 Metal")]
        private void Add50Metal() => AddItem(InventoryItemType.Metal, 50);
        
        [ContextMenu("Remove 50 Metal")]
        private void Remove50Metal() => RemoveItem(InventoryItemType.Metal, 50);

#endif
    }
}
