using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spotnose.Stardust
{
    public enum ItemType { Metal }

    public class Inventory : MonoBehaviour
    {
        private int _metal;

        public int GetItemCount(ItemType itemType)
        {
            return itemType switch
            {
                ItemType.Metal => _metal,
                _ => 0
            };
        }

        public void AddItem(ItemType itemType, int amount)
        {
            switch (itemType)
            {
                case ItemType.Metal:
                    _metal += amount;
                    Events.OnMetalCountChanged.Invoke(_metal);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(itemType), itemType, null);
            }
        }
        
        public void RemoveItem(ItemType itemType, int amount)
        {
            switch (itemType)
            {
                case ItemType.Metal:
                    _metal -= amount;
                    Events.OnMetalCountChanged.Invoke(_metal);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(itemType), itemType, null);
            }
        }
    }
}
