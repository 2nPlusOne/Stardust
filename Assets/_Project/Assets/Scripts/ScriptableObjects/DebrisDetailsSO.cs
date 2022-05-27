using System.Collections.Generic;
using UnityEngine;

namespace Spotnose.Stardust
{
    [CreateAssetMenu(fileName = "DebrisDetails_", menuName = "Scriptable Objects/Debris Details")]
    public class DebrisDetailsSO : ScriptableObject
    {
        [Space(10)]
        [Header("DEBRIS BASIC CONFIGURATION")]
        public CelestialBodyType celestialBodyType;
        public SizeCategory sizeCategory;
        public Sprite sprite;

        [Tooltip("The prefabs to use for this debris")]
        public List<GameObject> debrisPrefabs;

        [Header("ABSORPTION CONFIGURATION")]
        [Tooltip("Minimum impact relative velocity needed to absorb this debris")]
        public float absorbRelativeVelocityMin;

        [Tooltip("Reward for collecting this debris")]
        public int absorbReward;

        [Space(10)]
        [Header("SPAWNING DETAILS")]
        [Tooltip("The minimum and maximum speed this debris is initialized with")]
        public Vector2 initialSpeedRange;
        
        [Tooltip("The maximum number of this debris allowed active at once")]
        public int maxParticulateCount = 100;
        
        [Tooltip("The time interval at which spawn attempts for this debris are made")]
        public float spawnInterval = 0.01f;
        
        [HideInInspector] public int activeDebrisCount;
        [HideInInspector] public float debrisSpawnTimer;
        
        public GameObject GetRandomPrefab() => debrisPrefabs[UnityEngine.Random.Range(0, debrisPrefabs.Count)];
        public float GetRandomSpeed() => UnityEngine.Random.Range(initialSpeedRange.x, initialSpeedRange.y);
        
        /// <summary>
        /// Returns the size order of this body. The size order is defined by (int) CelestialBodyType + (int) SizeCategory.
        /// </summary>
        public int GetSizeOrder() => (int) celestialBodyType + (int) sizeCategory;
    }
}