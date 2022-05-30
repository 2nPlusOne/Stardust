using UnityEngine;

namespace Spotnose.Stardust
{
    public class DustParticulate : Particulate
    {
        protected override void HandleCollision(Collision2D collision)
        {
            var mass = collision.gameObject.GetComponentInParent<Mass>();
            if (mass is null) return;
            if (collision.relativeVelocity.magnitude < ParticulateDetails.pickupRelativeVelocityMin) return;
            
            mass.AddMass(ParticulateDetails.pickupReward);
            SoundManager.Instance.PlaySoundEffect(ParticulateDetails.pickupSound);
            print($"Dust collected! Mass: {mass.GetCurrentMass()}");
            
            gameObject.SetActive(false);
        }
    }
}