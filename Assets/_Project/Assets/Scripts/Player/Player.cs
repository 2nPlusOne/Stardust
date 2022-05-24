using UnityEngine;

namespace Spotnose.Stardust
{
    [RequireComponent(typeof(Health), typeof(Mass), typeof(Inventory))]
    [DisallowMultipleComponent]
    public class Player : Singleton<Player>
    {
        [SerializeField] private Transform bodyParentTransform;
        [SerializeField] private Transform engineParentTransform;
        public BodyDetailsSO BodyDetails { get; private set; }
        public Health Health { get; private set; }
        public Mass Mass { get; private set; }
        public Inventory Inventory { get; private set; }
        public Engine CurrentEngine { get; private set; }
        public Rigidbody2D Rb2d { get; private set; }

        private void OnEnable()
        {
            Events.OnMassReachedMax.AddListener(OnMassReachedMax);
            Events.OnMassReachedMin.AddListener(OnMassReachedMin);
        }
        
        private void OnDisable()
        {
            Events.OnMassReachedMax.RemoveListener(OnMassReachedMax);
            Events.OnMassReachedMin.RemoveListener(OnMassReachedMin);
        }

        protected override void Awake()
        {
            base.Awake();
            Health = GetComponent<Health>();
            Mass = GetComponent<Mass>();
            Inventory = GetComponent<Inventory>();
            CurrentEngine = GetComponentInChildren<Engine>();
            Rb2d = GetComponent<Rigidbody2D>();
        }

        public void SetBodyDetails(BodyDetailsSO bodyDetails)
        {
            BodyDetails = bodyDetails;
            Mass.SetBodyDetails(BodyDetails);
            Health.SetMaxHealth(BodyDetails.baseHealth);
        }

        private void OnMassReachedMax(Mass mass)
        {
            var bodyDetails = mass.GetBodyDetails();
            if (!bodyDetails.hasNextBodyDetails) return;
            Instance.SetBodyDetails(bodyDetails.nextBodyDetails);
            SetBody(bodyDetails.nextBodyDetails.bodyPrefab);
        }

        private void OnMassReachedMin(Mass mass)
        {
            var bodyDetails = mass.GetBodyDetails();
            if (!bodyDetails.hasPreviousBodyDetails) return;
            Instance.SetBodyDetails(bodyDetails.previousBodyDetails);
            SetBody(bodyDetails.previousBodyDetails.bodyPrefab);
        }

        private void SetBody(GameObject bodyPrefab)
        {
            print("Setting body to: " + bodyPrefab.name.ToString().Trim() + ".");
            foreach (Transform child in bodyParentTransform) Destroy(child.gameObject);
            var newBody = Instantiate(bodyPrefab, bodyParentTransform);
        }
        
        private void SetEngine(GameObject enginePrefab)
        {
            
        }
    }
}
