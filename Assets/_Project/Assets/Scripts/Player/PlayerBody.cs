using UnityEngine;

namespace Spotnose.Stardust
{
    [RequireComponent(typeof(Health), typeof(Mass), typeof(Inventory))]
    [DisallowMultipleComponent]
    public class PlayerBody : Singleton<PlayerBody>
    {
        public BodyDetailsSO BodyDetails { get; private set; }
        public Health Health { get; private set; }
        public Mass Mass { get; private set; }
        public Inventory Inventory { get; private set; }
        public Engine CurrentEngine { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Health = GetComponent<Health>();
            Mass = GetComponent<Mass>();
            Inventory = GetComponent<Inventory>();
            CurrentEngine = GetComponentInChildren<Engine>();
        }
        
        public void SetBodyDetails(BodyDetailsSO bodyDetails)
        {
            BodyDetails = bodyDetails;
            Health.SetMaxHealth(BodyDetails.baseHealth);
            Mass.SetBodyDetails(BodyDetails);
        }
    }
}
