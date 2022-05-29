using UnityEngine;

namespace Spotnose.Stardust
{
    [DisallowMultipleComponent]
    public class Mass : MonoBehaviour
    {
        private BodyDetailsSO _bodyDetails;
        private int _currentMass;

        public BodyDetailsSO GetBodyDetails() => _bodyDetails;
        
        public void SetBodyDetails(BodyDetailsSO bodyDetails)
        {
            _bodyDetails = bodyDetails;
        }

        public int GetCurrentMass() => _currentMass;

        /// <summary>
        /// Allowed a user to add mass to this mass component, increasing its current mass.
        /// </summary>
        /// <param name="amount"> The amount to increase the current mass by.</param>
        public void AddMass(int amount)
        {
            if (_currentMass >= _bodyDetails.maxMass) return;
            _currentMass += amount;
            
            if (_currentMass >= _bodyDetails.maxMass) Events.OnMassReachedMax.Invoke(this);

            Events.OnMassChanged.Invoke(this);
        }

        /// <summary>
        /// Allowed a user to reduce the current mass of this mass component.
        /// </summary>
        /// <param name="amount"> The amount to reduce the current mass by.</param>
        public void ReduceMass(int amount)
        {
            if (_currentMass - amount < 0)
            {
                _currentMass = 0;
                Events.OnMassReachedZero.Invoke(this);
                return;
            }
            //if (_currentMass <= _bodyDetails.minMass) return;
            _currentMass -= amount;
            
            if (_currentMass <= _bodyDetails.minMass) Events.OnMassReachedMin.Invoke(this);
            
            Events.OnMassChanged.Invoke(this);
        }

        /// Set the current mass to the specified percentage of the max mass.
        /// <param name="percentage"> A value between 0 and 1 specifying the percentage of the max mass to set the current mass to.</param>
        public void SetMassPercentage(float percentage)
        {
            if (percentage is < 0 or > 1) return;
            _currentMass = Mathf.RoundToInt(percentage * _bodyDetails.maxMass);
            
            if (_currentMass >= _bodyDetails.maxMass) Events.OnMassReachedMax.Invoke(this);
            if (_currentMass <= _bodyDetails.minMass) Events.OnMassReachedMin.Invoke(this);

            Events.OnMassChanged.Invoke(this);
        }

        public float GetMassPercentage()
        {
            var massDifference = _bodyDetails.maxMass - _bodyDetails.minMass;
            return (float) (_currentMass - _bodyDetails.minMass) / massDifference;
        }
        
#if UNITY_EDITOR
        
        [ContextMenu("Add 5 Mass")]
        private void Add5Mass() => AddMass(5);
        
        [ContextMenu("Reduce 5 Mass")]
        private void Reduce5Mass() => ReduceMass(5);
        
        [ContextMenu("Add 10 Mass")]
        private void AddMass10() => AddMass(10);
        
        [ContextMenu("Reduce 10 Mass")]
        private void ReduceMass10() => ReduceMass(10);
        
        [ContextMenu("Add 20 Mass")]
        private void AddMass20() => AddMass(20);
        
        [ContextMenu("Reduce 20 Mass")]
        private void ReduceMass20() => ReduceMass(20);
        
        [ContextMenu("Add 30 Mass")]
        private void AddMass30() => AddMass(30);
        
        [ContextMenu("Reduce 30 Mass")]
        private void ReduceMass30() => ReduceMass(30);
        
        [ContextMenu("Add 60 Mass")]
        private void AddMass60() => AddMass(60);
        
        [ContextMenu("Reduce 60 Mass")]
        private void ReduceMass60() => ReduceMass(60);

#endif
    }
}