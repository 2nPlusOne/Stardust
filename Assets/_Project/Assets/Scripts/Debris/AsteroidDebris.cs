using UnityEngine;

namespace Spotnose.Stardust
{
    [DisallowMultipleComponent]
    public class AsteroidDebris : Debris
    {
        [SerializeField] private float collisionDebounceTime = 0.25f;
        private float _collisionDebounceTimer = 0f;
        protected override void HandleCollision(Collision2D collision) 
        {
            var relativeVelocity = collision.relativeVelocity;
            // get player from collision if it exists
            var player = collision.gameObject.GetComponentInParent<Player>();
            // get asteroid from collision if it exists
            var asteroidDebris = collision.gameObject.GetComponent<AsteroidDebris>();

            if (asteroidDebris != null)
            {
                HandleSimpleCollision(relativeVelocity);
                return;
            }

            if (player == null) return;

            // debounce
            if (_collisionDebounceTimer > 0f) return;
            _collisionDebounceTimer = collisionDebounceTime;

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
            HandleSimpleCollision(relativeVelocity);
        }

        private void DamagePlayerMass(Player player, Vector2 relativeVelocity)
        {
            // If the relative velocity is above a threshold, deal damage to the player
            if (!(relativeVelocity.magnitude >= DebrisDetails.damageRelativeVelocityMin)) return;

            var damageModifier = relativeVelocity.magnitude / DebrisDetails.maxMassDamageRelativeVelocity;
            var damage = Mathf.RoundToInt(DebrisDetails.maxMassDamageOnImpact * damageModifier);
            player.Mass.ReduceMass(damage);
            print("Asteroid debris damaged player mass by " + damage);
            var volumeModifier = relativeVelocity.magnitude / Settings.ImpactRelativeVelocityForMaxVolume;
            Mathf.Clamp01(volumeModifier);
            SoundManager.Instance.PlaySoundEffect(DebrisDetails.impactSound, volumeModifier);
            // TODO: play particle effect
        }

        private void GetAbsorbedByPlayer(Player player, Vector2 relativeVelocity)
        {
            var mass = player.Mass;
            mass.AddMass(DebrisDetails.absorbReward);
            print("Asteroid absorbed by player for " + DebrisDetails.absorbReward + " mass");

            var volumeModifier = relativeVelocity.magnitude / Settings.ImpactRelativeVelocityForMaxVolume;
            Mathf.Clamp01(volumeModifier);
            SoundManager.Instance.PlaySoundEffect(DebrisDetails.absorbSound, volumeModifier);
            // TODO: play particle effect
            
            gameObject.SetActive(false);
        }

        private void HandleSimpleCollision(Vector2 relativeVelocity)
        {
            if (Vector3.Distance(Player.Instance.gameObject.transform.position, transform.position) > 8) return;
            var volumeModifier = relativeVelocity.magnitude / Settings.ImpactRelativeVelocityForMaxVolume;
            Mathf.Clamp01(volumeModifier);
            SoundManager.Instance.PlaySoundEffect(DebrisDetails.impactSound, volumeModifier);
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