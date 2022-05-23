using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spotnose
{
    public class DustParticulate : MonoBehaviour
    {
        [HideInInspector] public float timeAlive = 0.0f;
        
        [SerializeField] private ParticulateDetailsSO particulateDetails;

        private Rigidbody2D _rb2d;
        private PolygonCollider2D _collider;
        
        private void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _collider = GetComponent<PolygonCollider2D>();
        }

        public void Initialize(Vector3 spawnVelocity)
        {
            gameObject.SetActive(true);
            timeAlive = 0.0f;
            _rb2d.velocity = spawnVelocity;
        }

        private void Update()
        {
            timeAlive += Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D col) 
        {
            if (col.gameObject.TryGetComponent(out Mass mass))
            {
                mass.AddMass(particulateDetails.pickupReward);

                print($"Dust collected! Mass:{mass.GetCurrentMass()}");
                gameObject.SetActive(false);
            }
        }
    }
}
