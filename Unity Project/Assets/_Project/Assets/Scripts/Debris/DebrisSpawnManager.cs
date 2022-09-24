using System.Collections.Generic;
using UnityEngine;

namespace Spotnose.Stardust
{
    public class DebrisSpawnManager : MonoBehaviour
    {
        // TODO: Replace explicit logic for each debris with lists and loops
        // This script is horrendous and needs to be refactored BADLY!
        
        [Header("Spawn Radius")]
        [SerializeField] private float debrisMinSpawnRadius = 20f;
        [SerializeField] private float debrisMaxSpawnRadius = 150f;
        
        [Header("Debris Culling")]
        [SerializeField] private float debrisCullRadius = 151f;
        [SerializeField] private float debrisCullInterval = .01f;

        [Header("Debris Details")]
        [SerializeField] private DebrisDetailsSO asteroidDetails0;
        [SerializeField] private DebrisDetailsSO asteroidDetails1;
        [SerializeField] private DebrisDetailsSO asteroidDetails2;
        [SerializeField] private DebrisDetailsSO asteroidDetails3;
        [SerializeField] private DebrisDetailsSO asteroidDetails4;
        [SerializeField] private DebrisDetailsSO asteroidDetails5;
        [SerializeField] private DebrisDetailsSO asteroidDetails6;

        private Player _player;
        private List<Debris> _activeAsteroidDebris0 = new();
        private List<Debris> _activeAsteroidDebris1 = new();
        private List<Debris> _activeAsteroidDebris2 = new();
        private List<Debris> _activeAsteroidDebris3 = new();
        private List<Debris> _activeAsteroidDebris4 = new();
        private List<Debris> _activeAsteroidDebris5 = new();
        private List<Debris> _activeAsteroidDebris6 = new();
        private float _debrisCullTimer;
        
        private void Start()
        {
            _player = Player.Instance;
            asteroidDetails0.activeDebrisCount = 0;
            asteroidDetails1.activeDebrisCount = 0;
            asteroidDetails2.activeDebrisCount = 0;
            asteroidDetails3.activeDebrisCount = 0;
            asteroidDetails4.activeDebrisCount = 0;
            asteroidDetails5.activeDebrisCount = 0;
            asteroidDetails6.activeDebrisCount = 0;
        }
        
        private void Update()
        {
            _player ??= Player.Instance;  // TODO: mad sus

            HandleAsteroidDebris0();
            HandleAsteroidDebris1();
            HandleAsteroidDebris2();
            HandleAsteroidDebris3();
            HandleAsteroidDebris4();
            HandleAsteroidDebris5();
            HandleAsteroidDebris6();
            
            HandleDebrisCulling();
        }

        private void HandleAsteroidDebris0()
        {
            // Increment the timer
            asteroidDetails0.debrisSpawnTimer += Time.deltaTime;
            
            var spawnIntervalElapsed = asteroidDetails0.debrisSpawnTimer >= asteroidDetails0.spawnInterval;
            if (!spawnIntervalElapsed) return;
            asteroidDetails0.debrisSpawnTimer -= asteroidDetails0.spawnInterval;
            
            var maxDebrisReached = asteroidDetails0.activeDebrisCount >= asteroidDetails0.maxParticulateCount;
            if (!maxDebrisReached) SpawnAsteroidDebris0();
        }

        private void SpawnAsteroidDebris0()
        {
            var spawnPosition = Utilities.GenerateRandomPointOutsideCircle(_player.transform.position,
                debrisMinSpawnRadius, debrisMaxSpawnRadius);
            var spawnRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            var prefab = asteroidDetails0.GetRandomPrefab();

            var asteroidDebris0 = (Debris) PoolManager.Instance.GetObject(prefab, spawnPosition, spawnRotation);
            asteroidDebris0.Initialize(asteroidDetails0);
            
            _activeAsteroidDebris0.Add(asteroidDebris0);
            asteroidDetails0.activeDebrisCount++;
        }
        
        private void HandleAsteroidDebris1()
        {
            // Increment the timer
            asteroidDetails1.debrisSpawnTimer += Time.deltaTime;
            
            var spawnIntervalElapsed = asteroidDetails1.debrisSpawnTimer >= asteroidDetails1.spawnInterval;
            if (!spawnIntervalElapsed) return;
            asteroidDetails1.debrisSpawnTimer -= asteroidDetails1.spawnInterval;
            
            var maxDebrisReached = asteroidDetails1.activeDebrisCount >= asteroidDetails1.maxParticulateCount;
            if (!maxDebrisReached) SpawnAsteroidDebris1();
        }
        
        private void SpawnAsteroidDebris1()
        {
            var spawnPosition = Utilities.GenerateRandomPointOutsideCircle(_player.transform.position,
                debrisMinSpawnRadius, debrisMaxSpawnRadius);
            var spawnRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            var prefab = asteroidDetails1.GetRandomPrefab();

            var asteroidDebris1 = (Debris) PoolManager.Instance.GetObject(prefab, spawnPosition, spawnRotation);
            asteroidDebris1.Initialize(asteroidDetails1);
            
            _activeAsteroidDebris1.Add(asteroidDebris1);
            asteroidDetails1.activeDebrisCount++;
        }
        
        private void HandleAsteroidDebris2()
        {
            // Increment the timer
            asteroidDetails2.debrisSpawnTimer += Time.deltaTime;
            
            var spawnIntervalElapsed = asteroidDetails2.debrisSpawnTimer >= asteroidDetails2.spawnInterval;
            if (!spawnIntervalElapsed) return;
            asteroidDetails2.debrisSpawnTimer -= asteroidDetails2.spawnInterval;
            
            var maxDebrisReached = asteroidDetails2.activeDebrisCount >= asteroidDetails2.maxParticulateCount;
            if (!maxDebrisReached) SpawnAsteroidDebris2();
        }
        
        private void SpawnAsteroidDebris2()
        {
            var spawnPosition = Utilities.GenerateRandomPointOutsideCircle(_player.transform.position,
                debrisMinSpawnRadius, debrisMaxSpawnRadius);
            var spawnRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            var prefab = asteroidDetails2.GetRandomPrefab();

            var asteroidDebris2 = (Debris) PoolManager.Instance.GetObject(prefab, spawnPosition, spawnRotation);
            asteroidDebris2.Initialize(asteroidDetails2);
            
            _activeAsteroidDebris2.Add(asteroidDebris2);
            asteroidDetails2.activeDebrisCount++;
        }
        
        private void HandleAsteroidDebris3()
        {
            // Increment the timer
            asteroidDetails3.debrisSpawnTimer += Time.deltaTime;
            
            var spawnIntervalElapsed = asteroidDetails3.debrisSpawnTimer >= asteroidDetails3.spawnInterval;
            if (!spawnIntervalElapsed) return;
            asteroidDetails3.debrisSpawnTimer -= asteroidDetails3.spawnInterval;
            
            var maxDebrisReached = asteroidDetails3.activeDebrisCount >= asteroidDetails3.maxParticulateCount;
            if (!maxDebrisReached) SpawnAsteroidDebris3();
        }
        
        private void SpawnAsteroidDebris3()
        {
            var spawnPosition = Utilities.GenerateRandomPointOutsideCircle(_player.transform.position,
                debrisMinSpawnRadius, debrisMaxSpawnRadius);
            var spawnRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            var prefab = asteroidDetails3.GetRandomPrefab();

            var asteroidDebris3 = (Debris) PoolManager.Instance.GetObject(prefab, spawnPosition, spawnRotation);
            asteroidDebris3.Initialize(asteroidDetails3);
            
            _activeAsteroidDebris3.Add(asteroidDebris3);
            asteroidDetails3.activeDebrisCount++;
        }
        
        private void HandleAsteroidDebris4()
        {
            // Increment the timer
            asteroidDetails4.debrisSpawnTimer += Time.deltaTime;
            
            var spawnIntervalElapsed = asteroidDetails4.debrisSpawnTimer >= asteroidDetails4.spawnInterval;
            if (!spawnIntervalElapsed) return;
            asteroidDetails4.debrisSpawnTimer -= asteroidDetails4.spawnInterval;
            
            var maxDebrisReached = asteroidDetails4.activeDebrisCount >= asteroidDetails4.maxParticulateCount;
            if (!maxDebrisReached) SpawnAsteroidDebris4();
        }
        
        private void SpawnAsteroidDebris4()
        {
            var spawnPosition = Utilities.GenerateRandomPointOutsideCircle(_player.transform.position,
                debrisMinSpawnRadius, debrisMaxSpawnRadius);
            var spawnRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            var prefab = asteroidDetails4.GetRandomPrefab();

            var asteroidDebris4 = (Debris) PoolManager.Instance.GetObject(prefab, spawnPosition, spawnRotation);
            asteroidDebris4.Initialize(asteroidDetails4);
            
            _activeAsteroidDebris4.Add(asteroidDebris4);
            asteroidDetails4.activeDebrisCount++;
        }
        
        private void HandleAsteroidDebris5()
        {
            // Increment the timer
            asteroidDetails5.debrisSpawnTimer += Time.deltaTime;
            
            var spawnIntervalElapsed = asteroidDetails5.debrisSpawnTimer >= asteroidDetails5.spawnInterval;
            if (!spawnIntervalElapsed) return;
            asteroidDetails5.debrisSpawnTimer -= asteroidDetails5.spawnInterval;
            
            var maxDebrisReached = asteroidDetails5.activeDebrisCount >= asteroidDetails5.maxParticulateCount;
            if (!maxDebrisReached) SpawnAsteroidDebris5();
        }
        
        private void SpawnAsteroidDebris5()
        {
            var spawnPosition = Utilities.GenerateRandomPointOutsideCircle(_player.transform.position,
                debrisMinSpawnRadius, debrisMaxSpawnRadius);
            var spawnRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            var prefab = asteroidDetails5.GetRandomPrefab();

            var asteroidDebris5 = (Debris) PoolManager.Instance.GetObject(prefab, spawnPosition, spawnRotation);
            asteroidDebris5.Initialize(asteroidDetails5);
            
            _activeAsteroidDebris5.Add(asteroidDebris5);
            asteroidDetails5.activeDebrisCount++;
        }
        
        private void HandleAsteroidDebris6()
        {
            // Increment the timer
            asteroidDetails6.debrisSpawnTimer += Time.deltaTime;
            
            var spawnIntervalElapsed = asteroidDetails6.debrisSpawnTimer >= asteroidDetails6.spawnInterval;
            if (!spawnIntervalElapsed) return;
            asteroidDetails6.debrisSpawnTimer -= asteroidDetails6.spawnInterval;
            
            var maxDebrisReached = asteroidDetails6.activeDebrisCount >= asteroidDetails6.maxParticulateCount;
            if (!maxDebrisReached) SpawnAsteroidDebris6();
        }
        
        private void SpawnAsteroidDebris6()
        {
            var spawnPosition = Utilities.GenerateRandomPointOutsideCircle(_player.transform.position,
                debrisMinSpawnRadius, debrisMaxSpawnRadius);
            var spawnRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            var prefab = asteroidDetails6.GetRandomPrefab();

            var asteroidDebris6 = (Debris) PoolManager.Instance.GetObject(prefab, spawnPosition, spawnRotation);
            asteroidDebris6.Initialize(asteroidDetails6);
            
            _activeAsteroidDebris6.Add(asteroidDebris6);
            asteroidDetails6.activeDebrisCount++;
        }

        private void HandleDebrisCulling()
        {
            // Increment the timer
            _debrisCullTimer += Time.deltaTime;
            
            var cullIntervalElapsed = _debrisCullTimer > debrisCullInterval;
            if (!cullIntervalElapsed) return;
            _debrisCullTimer -= debrisCullInterval;
            
            CullDebris();
        }

        private void CullDebris()
        { 
            // Asteroid Debris 0
            var asteroidDebris0ToRemove = new List<Debris>();
            foreach (var asteroidDebris in _activeAsteroidDebris0)
            {
                if (!(Vector3.Distance(asteroidDebris.transform.position, _player.transform.position) >
                      debrisCullRadius)) continue;

                asteroidDebris.gameObject.SetActive(false);
                asteroidDetails0.activeDebrisCount--;
                asteroidDebris0ToRemove.Add(asteroidDebris);
            }
            foreach (var asteroidDebris in asteroidDebris0ToRemove) _activeAsteroidDebris0.Remove(asteroidDebris);
            
            // Asteroid Debris 1
            var asteroidDebris1ToRemove = new List<Debris>();
            foreach (var asteroidDebris in _activeAsteroidDebris1)
            {
                if (!(Vector3.Distance(asteroidDebris.transform.position, _player.transform.position) >
                      debrisCullRadius)) continue;

                asteroidDebris.gameObject.SetActive(false);
                asteroidDetails1.activeDebrisCount--;
                asteroidDebris1ToRemove.Add(asteroidDebris);
            }
            foreach (var asteroidDebris in asteroidDebris1ToRemove) _activeAsteroidDebris1.Remove(asteroidDebris);
            
            // Asteroid Debris 2
            var asteroidDebris2ToRemove = new List<Debris>();
            foreach (var asteroidDebris in _activeAsteroidDebris2)
            {
                if (!(Vector3.Distance(asteroidDebris.transform.position, _player.transform.position) >
                      debrisCullRadius)) continue;

                asteroidDebris.gameObject.SetActive(false);
                asteroidDetails2.activeDebrisCount--;
                asteroidDebris2ToRemove.Add(asteroidDebris);
            }
            foreach (var asteroidDebris in asteroidDebris2ToRemove) _activeAsteroidDebris2.Remove(asteroidDebris);
            
            // Asteroid Debris 3
            var asteroidDebris3ToRemove = new List<Debris>();
            foreach (var asteroidDebris in _activeAsteroidDebris3)
            {
                if (!(Vector3.Distance(asteroidDebris.transform.position, _player.transform.position) >
                      debrisCullRadius)) continue;

                asteroidDebris.gameObject.SetActive(false);
                asteroidDetails3.activeDebrisCount--;
                asteroidDebris3ToRemove.Add(asteroidDebris);
            }
            foreach (var asteroidDebris in asteroidDebris3ToRemove) _activeAsteroidDebris3.Remove(asteroidDebris);
            
            // Asteroid Debris 4
            var asteroidDebris4ToRemove = new List<Debris>();
            foreach (var asteroidDebris in _activeAsteroidDebris4)
            {
                if (!(Vector3.Distance(asteroidDebris.transform.position, _player.transform.position) >
                      debrisCullRadius)) continue;

                asteroidDebris.gameObject.SetActive(false);
                asteroidDetails4.activeDebrisCount--;
                asteroidDebris4ToRemove.Add(asteroidDebris);
            }
            foreach (var asteroidDebris in asteroidDebris4ToRemove) _activeAsteroidDebris4.Remove(asteroidDebris);
            
            // Asteroid Debris 5
            var asteroidDebris5ToRemove = new List<Debris>();
            foreach (var asteroidDebris in _activeAsteroidDebris5)
            {
                if (!(Vector3.Distance(asteroidDebris.transform.position, _player.transform.position) >
                      debrisCullRadius)) continue;

                asteroidDebris.gameObject.SetActive(false);
                asteroidDetails5.activeDebrisCount--;
                asteroidDebris5ToRemove.Add(asteroidDebris);
            }
            foreach (var asteroidDebris in asteroidDebris5ToRemove) _activeAsteroidDebris5.Remove(asteroidDebris);
            
            // Asteroid Debris 6
            var asteroidDebris6ToRemove = new List<Debris>();
            foreach (var asteroidDebris in _activeAsteroidDebris6)
            {
                if (!(Vector3.Distance(asteroidDebris.transform.position, _player.transform.position) >
                      debrisCullRadius)) continue;

                asteroidDebris.gameObject.SetActive(false);
                asteroidDetails6.activeDebrisCount--;
                asteroidDebris6ToRemove.Add(asteroidDebris);
            }
            foreach (var asteroidDebris in asteroidDebris6ToRemove) _activeAsteroidDebris6.Remove(asteroidDebris);
        }
    }
}
