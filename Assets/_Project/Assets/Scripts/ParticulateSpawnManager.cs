using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spotnose.Stardust
{
    [DisallowMultipleComponent]
    public class ParticulateSpawnManager : MonoBehaviour
    {
        [Header("Spawn Radius")]
        [SerializeField] private float particulateMinSpawnRadius = 10f;
        [SerializeField] private float particulateMaxSpawnRadius = 30f;
        
        [Header("Particulate Culling")]
        [SerializeField] private float particulateCullLifetime = 10f;
        [SerializeField] private float particulateCullRadius = 50f;
        [SerializeField] private float particulateCullInterval = 3f;

        [Header("Particulate Details")]
        [SerializeField] private ParticulateDetailsSO dustyDetails;

        private PlayerPlanet _playerPlanet;
        private List<DustParticulate> _activeDustParticulates = new();
        private float _particulateCullTimer;
        
        private void Start()
        {
            _playerPlanet = PlayerPlanet.Instance;
            dustyDetails.activeParticulateCount = 0;
        }
        
        private void Update()
        {
            HandleDustParticulates();
            HandleParticulateCulling();
        }

        private void HandleDustParticulates()
        {
            // Increment the timer
            dustyDetails.particulateSpawnTimer += Time.deltaTime;
            
            var spawnIntervalElapsed = dustyDetails.particulateSpawnTimer >= dustyDetails.spawnInterval;
            if (!spawnIntervalElapsed) return;
            dustyDetails.particulateSpawnTimer -= dustyDetails.spawnInterval;
            
            var maxParticulatesReached = dustyDetails.activeParticulateCount >= dustyDetails.maxParticulateCount;
            if (!maxParticulatesReached) SpawnDustParticulate();
        }
        
        private void SpawnDustParticulate()
        {
            var spawnPosition = Utilities.GenerateRandomPointOutsideCircle(_playerPlanet.transform.position,
                particulateMinSpawnRadius, particulateMaxSpawnRadius);
            var spawnRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            var prefab = dustyDetails.GetRandomPrefab();
            var spawnSpeed = dustyDetails.GetRandomSpeed();

            var dustParticulate = (DustParticulate) PoolManager.Instance.GetObject(prefab, spawnPosition, spawnRotation);
            var spawnVelocity = Utilities.GenerateRandomVectorOfMagnitude(spawnSpeed);
            dustParticulate.Initialize(spawnVelocity, dustyDetails);
            
            _activeDustParticulates.Add(dustParticulate);
            dustyDetails.activeParticulateCount++;
        }

        private void HandleParticulateCulling()
        {
            // Increment the timer
            _particulateCullTimer += Time.deltaTime;
            
            var cullIntervalElapsed = _particulateCullTimer > particulateCullInterval;
            if (!cullIntervalElapsed) return;
            _particulateCullTimer -= particulateCullInterval;
            
            CullParticulates();
        }

        private void CullParticulates()
        {
            var particulatesToRemove = new List<DustParticulate>();
            foreach (var particulate in _activeDustParticulates)
            {
                if (!(Vector3.Distance(particulate.transform.position, _playerPlanet.transform.position) >
                      particulateCullRadius)) continue;
                
                if (particulate.timeAlive < particulateCullLifetime) continue;
                
                particulate.gameObject.SetActive(false);
                particulatesToRemove.Add(particulate);
                dustyDetails.activeParticulateCount--;
            }
            
            foreach (var particulate in particulatesToRemove) _activeDustParticulates.Remove(particulate);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;
            var playerPosition = _playerPlanet.transform.position;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerPosition, particulateCullRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(playerPosition, particulateMinSpawnRadius);
            Gizmos.DrawWireSphere(playerPosition, particulateMaxSpawnRadius);
        }
#endif
    }
}
