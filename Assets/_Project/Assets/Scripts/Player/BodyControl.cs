using System;
using UnityEngine;

namespace Spotnose.Stardust
{
    [RequireComponent(typeof(Player))]
    [DisallowMultipleComponent]
    public class BodyControl : MonoBehaviour
    {
        // engine pivot
        [SerializeField] private Transform enginePivotTransform;

        private Player _player;
        private Engine _engine;
        private Rigidbody2D _rb2d;
        private float _turnInput;
        private float _thrustInput;
        private Vector2 _engineForceDirection;
        private Vector2 _movementVector;

        private void Awake()
        {
            _player = GetComponent<Player>();
            _rb2d = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _engine = _player.CurrentEngine;
        }

        private void Update()
        {
            _turnInput = Input.GetAxis("Horizontal");
            _thrustInput = Input.GetAxis("Vertical");
            
            HandleParticles();

            _engineForceDirection = enginePivotTransform.up;
            _movementVector = _engineForceDirection * (_engine.engineForce * _thrustInput);
        }

        private void FixedUpdate()
        {
            _rb2d.AddForce(_movementVector);
            _rb2d.velocity = Vector2.ClampMagnitude(_rb2d.velocity, _engine.engineMaxSpeed);
        
            enginePivotTransform.Rotate(Vector3.forward * (_engine.engineTurnSpeed * _turnInput));
        }
        
        public void SetEngine(Engine engine)
        {
            if (_engine.engineParticles.isPlaying)
                _engine.engineParticles.Stop();
            
            _engine = engine;
        }

        private void HandleParticles()
        {
            if (_thrustInput > .1 && !_engine.engineParticles.isPlaying)
            {
                _engine.engineParticles.Play();
            }
            else if (_thrustInput < .1 && _engine.engineParticles.isPlaying)
            {
                _engine.engineParticles.Stop();
            }
        }
    }

}