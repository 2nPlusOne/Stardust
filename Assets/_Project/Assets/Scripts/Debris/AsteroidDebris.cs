using UnityEngine;

namespace Spotnose.Stardust
{
    public class AsteroidDebris : Debris
    {
        [SerializeField] private float collisionDebounceTime = 0.25f;
        private float _collisionDebounceTimer = 0f;
        protected override void HandleCollision(Collision2D collision) 
        {
            // get player from collision
            var player = collision.gameObject.GetComponentInParent<Player>();
            if (player == null) return;
            
            // debounce
            if (_collisionDebounceTimer > 0f) return;
            _collisionDebounceTimer = collisionDebounceTime;

            var relativeVelocity = collision.relativeVelocity;
            print(player.BodyDetails);
            var canBeAbsorbed = player.BodyDetails.sizeOrder >= DebrisDetails.minAbsorptionSizeOrder;
            
            // If the player isn't big enough, deal damage to them
            if (!canBeAbsorbed)
            {
                DamagePlayerMass(player, relativeVelocity);
                return;
            }

            // if not, see if the relative velocity is high enough to be absorbed
            if (collision.relativeVelocity.magnitude >= DebrisDetails.absorbRelativeVelocityMin)
            {
                GetAbsorbedByPlayer(player, relativeVelocity);
                return;
            }
            
            // otherwise, just do a simple collision without absorbing or dealing damage
            HandleSimpleCollision(player, relativeVelocity);
        }

        private void DamagePlayerMass(Player player, Vector2 relativeVelocity)
        {
            var damageModifier = relativeVelocity.magnitude / DebrisDetails.maxMassDamageRelativeVelocity;
            var damage = Mathf.RoundToInt(DebrisDetails.maxMassDamageOnImpact * damageModifier);
            player.Mass.ReduceMass(damage);
            print("Asteroid debris damaged player mass by " + damage);
            // TODO: play sound
            // TODO: play particle effect
        }

        private void GetAbsorbedByPlayer(Player player, Vector2 relativevelocity)
        {
            var mass = player.Mass;
            mass.AddMass(DebrisDetails.absorbReward);
            print("Asteroid absorbed by player");
            // TODO: play sound
            // TODO: play particle effect
            gameObject.SetActive(false);
        }

        private void HandleSimpleCollision(Player player, Vector2 relativeVelocity)
        {
            // TODO: play low velocity asteroid impact sound
            // TODO: play low velocity asteroid impact particle effect
        }

        private void Update()
        {
            if (_collisionDebounceTimer > 0f)
            {
                _collisionDebounceTimer -= Time.deltaTime;
            }
        }
    }
}