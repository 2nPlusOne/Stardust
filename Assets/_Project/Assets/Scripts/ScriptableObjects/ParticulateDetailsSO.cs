using System.Collections.Generic;
using UnityEngine;

namespace Spotnose.Stardust
{
    public enum ParticulateType { DustParticulate, MetalParticulate, WaterParticulate }
    
    [CreateAssetMenu(fileName = "ParticulateDetails_", menuName = "Scriptable Objects/Particulate Details")]
    public class ParticulateDetailsSO : ScriptableObject
    {
        [Space(10)]
        [Header("BASE DETAILS")]
        
        [Tooltip("The name of the particulate")]
        public string particulateName;

        [Tooltip("The type of particulate this is")]
        public ParticulateType particulateType;
        
        [Tooltip("Minimum impact relative velocity needed to collect this particulate")]
        public float pickupRelativeVelocityMin;
        
        [Tooltip("Reward for collecting this particulate")]
        public int pickupReward;
        
        [Tooltip("The prefabs to use for this particulate")]
        public List<GameObject> particulatePrefabs;
        
        [Space(10)]
        [Header("PARTICULATE STATS")]
        [Tooltip("The minimum and maximum speed this particulate is initialized with")]
        public Vector2 initialSpeedRange;
        
        [Tooltip("The maximum number of this particulate allowed active at once")]
        public int maxParticulateCount = 100;
        
        [Tooltip("The time interval at which spawn attempts for this particulate are made")]
        public float spawnInterval = 0.01f;
        
        [HideInInspector] public int activeParticulateCount;
        [HideInInspector] public float particulateSpawnTimer;
        
        public GameObject GetRandomPrefab() => particulatePrefabs[UnityEngine.Random.Range(0, particulatePrefabs.Count)];
        public float GetRandomSpeed() => UnityEngine.Random.Range(initialSpeedRange.x, initialSpeedRange.y);
    }
}
