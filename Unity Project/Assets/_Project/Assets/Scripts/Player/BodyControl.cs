using System;
using UnityEngine;

namespace Spotnose.Stardust
{
    [RequireComponent(typeof(Player), typeof(InputHandler))]
    [RequireComponent(typeof(AudioSource))]
    [DisallowMultipleComponent]
    public class BodyControl : MonoBehaviour
    {
        // engine pivot
        [SerializeField] private Transform enginePivotTransform;

        private InputHandler _inputHandler;
        private Player _player;
        private Engine _engine;
        private Rigidbody2D _rb2d;
        private float _turnInput;
        private float _thrustInput;
        private Vector2 _engineForceDirection;
        private Vector2 _movementVector;
        
        private bool _invertSteering;

        private void OnEnable()
        {
            Events.OnGameStarted.AddListener(OnGameStarted);
            Events.OnInvertSteeringChanged.AddListener(OnInvertSteeringChanged);
        }
        
        private void OnDisable()
        {
            Events.OnGameStarted.RemoveListener(OnGameStarted);
            Events.OnInvertSteeringChanged.RemoveListener(OnInvertSteeringChanged);
        }

        private void OnInvertSteeringChanged(bool inverted)
        {
            print("Invert steering changed to " + inverted);
            _invertSteering = inverted;
        }

        private void Awake()
        {
            _inputHandler = GetComponent<InputHandler>();
            _player = GetComponent<Player>();
            _rb2d = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _engine = _player.CurrentEngine;
            _invertSteering = PlayerPrefs.GetInt("InvertSteering", 0) == 1;
        }

        private void Update()
        {
            _turnInput = _inputHandler.GetTurnInput();
            _thrustInput = _inputHandler.GetThrustInput();
            
            HandleParticles();

            _engineForceDirection = enginePivotTransform.up;
            _movementVector = _engineForceDirection * (_engine.engineForce * _thrustInput);
        }

        private void FixedUpdate()
        {
            _rb2d.AddForce(_movementVector);
            _rb2d.velocity = Vector2.ClampMagnitude(_rb2d.velocity, _engine.engineMaxSpeed);
            
            enginePivotTransform.Rotate(Vector3.forward * (_engine.engineTurnSpeed * _turnInput * (_invertSteering ? 1 : -1)));
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

        // TODO: fix this function (doesn't work at all right now)
        private void OnGameStarted(GameObject _)
        {
            // // Set the player's initial velocity to a random vector of magnitude 2
            // var initialVelocity = new Vector2(
            //     UnityEngine.Random.Range(-1f, 1f),
            //     UnityEngine.Random.Range(-1f, 1f)
            // );
            // initialVelocity.Normalize();
            // initialVelocity *= 2f;
            // _rb2d.velocity = initialVelocity;
        }
    }

}