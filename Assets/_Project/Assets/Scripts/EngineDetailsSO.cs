using UnityEngine;

namespace Spotnose
{
    [CreateAssetMenu(fileName = "EngineDetails_", menuName = "Scriptable Objects/Engine Details")]
    public class EngineDetailsSO : ScriptableObject
    {
        [Header("ENGINE BASIC CONFIGURATION")]
        public string engineName;
        
        [Range(0, 3)]
        public int engineUpgradeLevel;
        
        [Tooltip("The prefab that will be instantiated when the engine is purchased and parented to the EnginePivot on the player's body")]
        public GameObject enginePrefab;
        
        [Tooltip("The sound that will be played when the engine is activated")]
        public AudioClip engineActivationSound;

        [Header("ENGINE BASE STATS")]
        public float engineTurnSpeed;
        public float engineForce;
        public float engineMaxSpeed;
    }
}
