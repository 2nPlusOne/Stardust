using UnityEngine;

namespace Spotnose.Stardust
{
    public class MetalParticulate : Particulate
    {
        protected override void HandleCollision(Collision2D collision)
        {
            var inventory = collision.gameObject.GetComponentInParent<Inventory>();
            if (inventory is null) return;
            
            inventory.AddItem(ItemType.Metal, ParticulateDetails.pickupReward);
            print($"Metal collected! Metal: {inventory.GetItemCount(ItemType.Metal)}");
            
            gameObject.SetActive(false);
        }
    }
}