using UnityEngine;

namespace Spotnose.Stardust
{
    public class DustParticulate : MonoBehaviour
    {
        [HideInInspector] public float timeAlive = 0.0f;
        
        private ParticulateDetailsSO _particulateDetails;
        private Rigidbody2D _rb2d;
        private PolygonCollider2D _collider;
        
        private void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _collider = GetComponent<PolygonCollider2D>();
        }

        public void Initialize(Vector3 spawnVelocity, ParticulateDetailsSO particulateDetails)
        {
            gameObject.SetActive(true);
            _particulateDetails = particulateDetails;
            timeAlive = 0.0f;
            _rb2d.velocity = spawnVelocity;
        }

        private void Update()
        {
            timeAlive += Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            var mass = col.gameObject.GetComponentInParent<Mass>();
            if (mass is null) return;
            if (col.relativeVelocity.magnitude < _particulateDetails.pickupRelativeVelocityMin) return;
            
            mass.AddMass(_particulateDetails.pickupReward);

            print($"Dust collected! Mass:{mass.GetCurrentMass()}");
            gameObject.SetActive(false);
        }
    }
}
