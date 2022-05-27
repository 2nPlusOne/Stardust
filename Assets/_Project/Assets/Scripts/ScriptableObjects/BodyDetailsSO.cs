using UnityEngine;

namespace Spotnose.Stardust
{
    public enum CelestialBodyType { Asteroid = 0, Moon = 10, Planet = 20}

    public enum SizeCategory
    {
        Minuscule = 0,
        Tiny = 1, 
        Small = 2, 
        Medium = 3, 
        Large = 4, 
        Huge = 5, 
        Gigantic = 6, 
        Enormous = 7, 
        Colossal = 8
    }
    
    [CreateAssetMenu(fileName = "BodyDetails_", menuName = "Scriptable Objects/Body Details")]
    public class BodyDetailsSO : ScriptableObject
    {
        [Header("BODY BASIC CONFIGURATION")]
        public CelestialBodyType celestialBodyType;
        public SizeCategory sizeCategory;
        public Sprite sprite;

        [Tooltip("The prefab to use for this body that will be parented to the PlayerBody game object.")]
        public GameObject bodyPrefab;

        [Header("BODY SIZE PROGRESSION")]
        [Range(0, 6)]
        [Tooltip("The position of this body in the game's size progression. One body SO is made for each progression position." +
                 "Zero is for the first and smallest body, and the highest int is for the largest.")]
        public int progressionPosition;
        
        [Tooltip("Check this if the body has a previous BodyDetailsSO. This is used when switching the body to the previous size.")]
        public bool hasPreviousBodyDetails;

        [Tooltip("The BodyDetailsSO of the previous size. This is used when switching the body to the previous size.")]
        public BodyDetailsSO previousBodyDetails;
        
        [Tooltip("Check this if the body has a next BodyDetailsSO. This is used when switching the body to the next size.")]
        public bool hasNextBodyDetails;
        
        [Tooltip("The BodyDetailsSO of the next size. This is used when switching the body to the next size.")]
        public BodyDetailsSO nextBodyDetails;

        [Header("BODY BASE STATS")]
        [Tooltip("The base health of this body.")]
        public int baseHealth;
        
        [Tooltip("The mass below which before the player's body breaks and downgrades to the body with the previous size.")]
        public int minMass;
        
        [Tooltip("The mass that needs to be accumulated before the player can move to the next body size " +
                 "-- the body with the position in the progression.")]
        public int maxMass;
        
        [Tooltip("The rigidbody mass of this body when the mass is at minMass.")]
        public float minRigidbodyMass;
        
        [Tooltip("The rigidbody mass of this body when the mass is at maxMass.")]
        public float maxRigidbodyMass;

        [Tooltip("The transform scale of this body when the mass is at maxMass. The scale should be reset when the body progresses to the next size.")]
        public float maxTransformScale;
        
        [Tooltip("The base Y offset to use for the engine sprite position when this body is active. As the scale grows this value will be multiplied by the scale.")]
        public float engineSpriteBaseYOffset;

        /// <summary>
        /// Returns the size order of this body. The size order is defined by (int) CelestialBodyType + (int) SizeCategory.
        /// </summary>
        public int GetSizeOrder() => (int) celestialBodyType + (int) sizeCategory;
    }
}