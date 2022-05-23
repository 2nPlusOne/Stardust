using UnityEngine;

namespace Spotnose.Stardust
{
    [RequireComponent(typeof(Rigidbody2D))]
    [DisallowMultipleComponent]
    public class Mass : MonoBehaviour
    {
        private BodyDetailsSO _bodyDetails;
        private Rigidbody2D _rb2d;
        [SerializeField] private int _minMass;
        [SerializeField] private int _maxMass;
        [SerializeField]private int _currentMass;

        private void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
        }

        public BodyDetailsSO GetBodyDetails() => _bodyDetails;
        
        public void SetBodyDetails(BodyDetailsSO bodyDetails)
        {
            _bodyDetails = bodyDetails;
            _minMass = _bodyDetails.minMass;
            _maxMass = bodyDetails.maxMass;
            _currentMass = bodyDetails.minMass;
        }

        /// <summary>
        /// Allowed a user to reduce the current mass of this mass component.
        /// </summary>
        /// <param name="amount"> The amount to reduce the current mass by.</param>
        /// <returns> A bool for whether the reduction caused the current mass to be less than or equal to the min mass.</returns>
        public bool ReduceMass(int amount)
        {
            if (_currentMass <= _minMass) return false;
            _currentMass -= amount;
            // set the rigid body mass to a
            return _currentMass <= 0;
        }
        
        public int GetCurrentMass() => _currentMass;
        
        /// <summary>
        /// Allowed a user to add mass to this mass component, increasing its current mass.
        /// </summary>
        /// <param name="amount"> The amount to increase the current mass by.</param>
        /// <returns> A bool for whether the increase caused the current mass to be greater than or equal to the max mass.</returns>
        public bool AddMass(int amount)
        {
            if (_currentMass >= _maxMass) return false;
            _currentMass += amount;
            Events.OnMassChanged.Invoke(this);
            if (_currentMass >= _maxMass)
            {
                _currentMass = _maxMass;
                
                return true;
            }
            return false;
        }

        /// Set the current mass to the specified percentage of the max mass.
        /// <param name="percentage"> A value between 0 and 1 specifying the percentage of the max mass to set the current mass to.</param>
        public void SetMassPercentage(float percentage)
        {
            _currentMass = Mathf.RoundToInt(percentage * _maxMass);
        }
        
        public float GetMassPercentage() => (float) _currentMass / _maxMass;
    }
}