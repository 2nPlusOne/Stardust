using UnityEngine;

namespace Spotnose.Stardust
{
    public class MetalParticulate : Particulate
    {
        protected override void HandleCollision(Collision2D collision)
        {
            var inventory = collision.gameObject.GetComponentInParent<Inventory>();
            if (inventory is null) return;
            
            inventory.AddItem(InventoryItemType.Metal, ParticulateDetails.pickupReward);
            print($"Metal collected! Metal: {inventory.GetItemCount(InventoryItemType.Metal)}");
            
            gameObject.SetActive(false);
        }
    }
}