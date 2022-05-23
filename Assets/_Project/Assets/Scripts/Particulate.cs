using UnityEngine;

namespace Spotnose.Stardust
{
    public class Particulate : MonoBehaviour
    {
        [HideInInspector] public float timeAlive = 0.0f;
        
        protected ParticulateDetailsSO ParticulateDetails;
        private Rigidbody2D _rb2d;
        private PolygonCollider2D _collider;
        
        protected virtual void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _collider = GetComponent<PolygonCollider2D>();
        }

        public virtual void Initialize(Vector3 spawnVelocity, ParticulateDetailsSO particulateDetails)
        {
            gameObject.SetActive(true);
            ParticulateDetails = particulateDetails;
            timeAlive = 0.0f;
            _rb2d.velocity = spawnVelocity;
        }

        protected virtual void Update()
        {
            timeAlive += Time.deltaTime;
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
