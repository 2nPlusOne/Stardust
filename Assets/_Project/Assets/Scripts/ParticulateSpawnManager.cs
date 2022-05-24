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
        [SerializeField] private ParticulateDetailsSO metalDetails;

        private Player _player;
        private List<Particulate> _activeDustParticulates = new();
        private List<Particulate> _activeMetalParticulates = new();
        private float _particulateCullTimer;
        
        private void Start()
        {
            _player = Player.Instance;
            dustyDetails.activeParticulateCount = 0;
            metalDetails.activeParticulateCount = 0;
        }
        
        private void Update()
        {
            _player ??= Player.Instance;  // TODO: replace player singleton this with dependency injection
            
            HandleDustParticulates();
            HandleMetalParticulates();
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
            var spawnPosition = Utilities.GenerateRandomPointOutsideCircle(_player.transform.position,
                particulateMinSpawnRadius, particulateMaxSpawnRadius);
            var spawnRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            var prefab = dustyDetails.GetRandomPrefab();
            var spawnSpeed = dustyDetails.GetRandomSpeed();

            var dustParticulate = (Particulate) PoolManager.Instance.GetObject(prefab, spawnPosition, spawnRotation);
            var spawnVelocity = Utilities.GenerateRandomVectorOfMagnitude(spawnSpeed);
            dustParticulate.Initialize(spawnVelocity, dustyDetails);
            
            _activeDustParticulates.Add(dustParticulate);
            dustyDetails.activeParticulateCount++;
        }

        private void HandleMetalParticulates()
        {
            // Increment the timer
            metalDetails.particulateSpawnTimer += Time.deltaTime;
            
            var spawnIntervalElapsed = metalDetails.particulateSpawnTimer >= metalDetails.spawnInterval;
            if (!spawnIntervalElapsed) return;
            metalDetails.particulateSpawnTimer -= metalDetails.spawnInterval;
            
            var maxParticulatesReached = metalDetails.activeParticulateCount >= metalDetails.maxParticulateCount;
            if (!maxParticulatesReached) SpawnMetalParticulate();
        }

        private void SpawnMetalParticulate()
        {
            var spawnPosition = Utilities.GenerateRandomPointOutsideCircle(_player.transform.position,
                particulateMinSpawnRadius, particulateMaxSpawnRadius);
            var spawnRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            var prefab = metalDetails.GetRandomPrefab();
            var spawnSpeed = metalDetails.GetRandomSpeed();
            
            var metalParticulate = (Particulate) PoolManager.Instance.GetObject(prefab, spawnPosition, spawnRotation);
            var spawnVelocity = Utilities.GenerateRandomVectorOfMagnitude(spawnSpeed);
            metalParticulate.Initialize(spawnVelocity, metalDetails);
            
            _activeMetalParticulates.Add(metalParticulate);
            metalDetails.activeParticulateCount++;
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
            // Dust Particulates
            var dustParticulatesToRemove = new List<Particulate>();
            foreach (var particulate in _activeDustParticulates)
            {
                if (!(Vector3.Distance(particulate.transform.position, _player.transform.position) >
                      particulateCullRadius)) continue;
                
                if (particulate.timeAlive < particulateCullLifetime) continue;
                
                particulate.gameObject.SetActive(false);
                dustParticulatesToRemove.Add(particulate);
                dustyDetails.activeParticulateCount--;
            }
            foreach (var particulate in dustParticulatesToRemove) _activeDustParticulates.Remove(particulate);
            
            // Metal Particulates
            var metalParticulatesToRemove = new List<Particulate>();
            foreach (var particulate in _activeMetalParticulates)
            {
                if (!(Vector3.Distance(particulate.transform.position, _player.transform.position) >
                      particulateCullRadius)) continue;
                
                if (particulate.timeAlive < particulateCullLifetime) continue;
                
                particulate.gameObject.SetActive(false);
                metalParticulatesToRemove.Add(particulate);
                metalDetails.activeParticulateCount--;
            }
            foreach (var particulate in metalParticulatesToRemove) _activeMetalParticulates.Remove(particulate);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;
            var playerPosition = _player.transform.position;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerPosition, particulateCullRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(playerPosition, particulateMinSpawnRadius);
            Gizmos.DrawWireSphere(playerPosition, particulateMaxSpawnRadius);
        }
#endif
    }
}
