using UnityEngine;

namespace Spotnose.Stardust
{
    public class UpdateRigidBodyMass : MonoBehaviour
    {
        private Rigidbody2D _rb2d;
        
        private void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            Events.OnMassChanged.AddListener(OnMassChanged);
        }
        
        private void OnDisable()
        {
            Events.OnMassChanged.RemoveListener(OnMassChanged);
        }
        
        private void OnMassChanged(Mass mass)
        {
            SetRigidbodyMass(mass);
        }

        private void SetRigidbodyMass(Mass mass)
        {
            var bodyDetails = mass.GetBodyDetails();
            var difference = bodyDetails.maxMass - bodyDetails.minMass;
            var massUntilMax = bodyDetails.maxMass - mass.GetCurrentMass();
            var massPercent = 1f - (float) massUntilMax / difference;
            
            var rbMassDifference = bodyDetails.maxRigidbodyMass - bodyDetails.minRigidbodyMass; 
            var rbMass = bodyDetails.minRigidbodyMass + (rbMassDifference * massPercent);
            _rb2d.mass = rbMass;
        }
    }
}
