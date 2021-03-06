using System.Collections.Generic;
using UnityEngine;

namespace Spotnose.Stardust
{
    [CreateAssetMenu(fileName = "DebrisDetails_", menuName = "Scriptable Objects/Debris Details")]
    public class DebrisDetailsSO : ScriptableObject
    {
        [Space(10)]
        [Header("DEBRIS BASIC CONFIGURATION")]
        public SizeOrder sizeOrder;
        public Sprite sprite;

        [Tooltip("The prefabs to use for this debris")]
        public List<GameObject> debrisPrefabs;

        [Header("ABSORPTION CONFIGURATION")]
        [Tooltip("The minimum size order that this debris can be absorbed by")]
        public SizeOrder minAbsorptionSizeOrder;
        
        [Tooltip("Minimum impact relative velocity needed to absorb this debris")]
        public float absorbRelativeVelocityMin;

        [Tooltip("Reward for collecting this debris")]
        public int absorbReward;
        
        [Header("MASS DAMAGE CONFIGURATION")]
        
        [Tooltip("Minimum impact relative velocity needed to deal damage to player's mass")]
        public float damageRelativeVelocityMin;
        
        [Tooltip("The maximum amount of damage this debris can inflict on the player's mass on impact. " +
                 "It will only be applied if the player doesn't meet the minimum size order requirements")]
        public int maxMassDamageOnImpact;
        
        [Tooltip("The maximum relative velocity needed to inflict maximum mass damage on the player. " +
                 "Impact damage is scaled by the relative velocity of the impact.")]
        public float maxMassDamageRelativeVelocity;

        [Space(10)]
        [Header("SPAWNING DETAILS")]
        [Tooltip("The minimum and maximum speed this debris is initialized with")]
        public Vector2 initialSpeedRange;
        
        [Tooltip("The maximum number of this debris allowed active at once")]
        public int maxParticulateCount = 100;
        
        [Tooltip("The time interval at which spawn attempts for this debris are made")]
        public float spawnInterval = 0.01f;
        
        [Header("SOUNDS")]
        [Tooltip("The sound to play when this debris is absorbed")]
        public SoundEffectSO absorbSound;
        
        [Tooltip("The sound to play when this debris is collided with (without being absorbed)")]
        public SoundEffectSO impactSound;
        
        [HideInInspector] public int activeDebrisCount;
        [HideInInspector] public float debrisSpawnTimer;

        public GameObject GetRandomPrefab() => debrisPrefabs[UnityEngine.Random.Range(0, debrisPrefabs.Count)];
        public float GetRandomSpeed() => UnityEngine.Random.Range(initialSpeedRange.x, initialSpeedRange.y);
    }
}