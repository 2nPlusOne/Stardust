using UnityEngine;

namespace Spotnose.Stardust
{
    public class Debris : MonoBehaviour
    {
        protected DebrisDetailsSO DebrisDetails;
        private Rigidbody2D _rb2d;
        private PolygonCollider2D _collider;
        
        protected virtual void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _collider = GetComponent<PolygonCollider2D>();
        }

        public void Initialize(DebrisDetailsSO debrisDetails)
        {
            var spawnSpeed = debrisDetails.GetRandomSpeed();
            var spawnVelocity = Utilities.GenerateRandomVectorOfMagnitude(spawnSpeed);
            gameObject.SetActive(true);
            DebrisDetails = debrisDetails;
            
            _rb2d.velocity = spawnVelocity;
            _rb2d.angularVelocity = Random.Range(45f, 270f);
        }
        
        protected void OnCollisionEnter2D(Collision2D collision)
        {
            HandleCollision(collision);
        }

        protected virtual void HandleCollision(Collision2D collision)
        {
            Debug.Log("Handling collision...");
        }
    }
}
