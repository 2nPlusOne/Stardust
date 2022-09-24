using UnityEngine;

namespace Spotnose.Stardust
{
    [CreateAssetMenu(fileName = "EngineDetails_", menuName = "Scriptable Objects/Engine Details")]
    public class EngineDetailsSO : ScriptableObject
    {
        [Header("ENGINE BASIC CONFIGURATION")]
        public string engineName;

        public Sprite engineIcon;
        
        [Tooltip("The prefab that will be instantiated when the engine is purchased and parented to the EnginePivot on the player's body")]
        public GameObject enginePrefab;

        [Header("UPGRADE CONFIGURATION")]
        [Range(0, 3)]
        public int engineUpgradeLevel;
        public InventoryItemType upgradeRequiresItemType;
        public int upgradeCost;

        [Tooltip("The sound that will be played when the engine is activated")]
        public AudioClip engineActivationSound;

        [Header("ENGINE BASE STATS")]
        public float engineTurnSpeed;
        public float engineForce;
        public float engineMaxSpeed;
    }
}
