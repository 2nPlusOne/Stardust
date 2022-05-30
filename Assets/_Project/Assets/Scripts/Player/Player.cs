using UnityEngine;

namespace Spotnose.Stardust
{
    [RequireComponent(typeof(Health), typeof(Mass), typeof(Inventory))]
    [RequireComponent(typeof(BodyControl), typeof(Rigidbody2D))]
    [DisallowMultipleComponent]
    public class Player : Singleton<Player>
    {
        [SerializeField] private Transform bodyParentTransform;
        [SerializeField] private Transform engineParentTransform;
        
        public PlayerStartingLoadoutSO startingLoadout;
        public BodyDetailsSO BodyDetails { get; private set; }
        public Health Health { get; private set; }
        public Mass Mass { get; private set; }
        public Inventory Inventory { get; private set; }
        public Engine CurrentEngine { get; private set; }
        public Rigidbody2D Rb2d { get; private set; }

        private BodyControl _bodyControl;

        private void OnEnable()
        {
            Events.OnMassReachedMax.AddListener(OnMassReachedMax);
            Events.OnMassReachedMin.AddListener(OnMassReachedMin);
            Events.OnEngineUpgradePurchased.AddListener(OnEngineUpgradePurchased);
        }

        private void OnDisable()
        {
            Events.OnMassReachedMax.RemoveListener(OnMassReachedMax);
            Events.OnMassReachedMin.RemoveListener(OnMassReachedMin);
            Events.OnEngineUpgradePurchased.RemoveListener(OnEngineUpgradePurchased);
        }

        protected override void Awake()
        {
            base.Awake();
            Health = GetComponent<Health>();
            Mass = GetComponent<Mass>();
            Inventory = GetComponent<Inventory>();
            CurrentEngine = GetComponentInChildren<Engine>();
            Rb2d = GetComponent<Rigidbody2D>();
            _bodyControl = GetComponent<BodyControl>();
        }

        public void Initialize()
        {
            SetBodyDetails(startingLoadout.startingBodyDetails);
            SetBody(startingLoadout.startingBodyDetails.bodyPrefab);
            SetEngine(startingLoadout.startingEngineDetails);
            Mass.SetCurrentMass(startingLoadout.startingMass);
            Inventory.SetItemCount(InventoryItemType.Metal, startingLoadout.startingMetal);
            transform.position = Vector3.zero;

            Events.OnBodyChanged.Invoke(startingLoadout.startingBodyDetails, Mass);
            Events.OnEngineChanged.Invoke(startingLoadout.startingEngineDetails);
        }

        private void SetBodyDetails(BodyDetailsSO bodyDetails)
        {
            BodyDetails = bodyDetails;
            Mass.SetBodyDetails(BodyDetails);
            Health.SetMaxHealth(BodyDetails.baseHealth);
        }

        private void OnMassReachedMax(Mass mass)
        {
            var bodyDetails = mass.GetBodyDetails();
            if (!bodyDetails.hasNextBodyDetails) return;
            SetBodyDetails(bodyDetails.nextBodyDetails);
            SetBody(bodyDetails.nextBodyDetails.bodyPrefab);
            Events.OnBodyChanged.Invoke(bodyDetails.nextBodyDetails, mass);
        }

        private void OnMassReachedMin(Mass mass)
        {
            var bodyDetails = mass.GetBodyDetails();
            if (!bodyDetails.hasPreviousBodyDetails) return;
            SetBodyDetails(bodyDetails.previousBodyDetails);
            SetBody(bodyDetails.previousBodyDetails.bodyPrefab);
            Events.OnBodyChanged.Invoke(bodyDetails.previousBodyDetails, mass);
        }

        private void SetBody(GameObject bodyPrefab)
        {
            print("Setting body to: " + bodyPrefab.name.Trim() + ".");
            foreach (Transform child in bodyParentTransform) Destroy(child.gameObject);
            var newBody = Instantiate(bodyPrefab, bodyParentTransform);
        }

        private void OnEngineUpgradePurchased(EngineDetailsSO engineDetails)
        {
            SetEngine(engineDetails);
        }

        private void SetEngine(EngineDetailsSO engineDetails)
        {
            print("Setting engine to: " + engineDetails.enginePrefab.name.Trim() + ".");
            var newEngine = Instantiate(engineDetails.enginePrefab, engineParentTransform).GetComponent<Engine>();
            _bodyControl.SetEngine(newEngine);
            Destroy(CurrentEngine.gameObject);
            CurrentEngine = newEngine;

            Events.OnEngineChanged.Invoke(engineDetails);
        }
    }
}
