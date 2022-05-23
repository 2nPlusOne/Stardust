using UnityEngine;

namespace Spotnose.Stardust
{
    public enum CelestialBodyType { Asteroid, Planet }
    
    [CreateAssetMenu(fileName = "PlanetDetails_", menuName = "Scriptable Objects/Planet Details")]
    public class PlanetDetailsSO : ScriptableObject
    {
        [Header("PLANET BASIC CONFIGURATION")]
        public string planetTypeName;
        
        public CelestialBodyType celestialBodyType;
        
        [Range(0, 6)]
        [Tooltip("The size class of this planet. One planet SO is made for each size class.")]
        public int sizeClass;

        [Tooltip("The prefab to use for this planet that will be parented to the PlayerPlanet game object.")]
        public GameObject planetPrefab;

        [Header("PLANET SIZE PROGRESSION")]
        [Tooltip("Check this if the planet has a previous PlanetDetailsSO. This is used when switching the planet to the previous size class.")]
        public bool hasPreviousPlanetDetails;
        
        [Tooltip("The PlanetDetailsSO of the previous size class. This is used when switching the planet to the previous size class.")]
        public PlanetDetailsSO previousPlanetDetails;
        
        [Tooltip("Check this if the planet has a next PlanetDetailsSO. This is used when switching the planet to the next size class.")]
        public bool hasNextPlanetDetails;
        
        [Tooltip("The PlanetDetailsSO of the next size class. This is used when switching the planet to the next size class.")]
        public PlanetDetailsSO nextPlanetDetails;

        [Header("ENGINE BASE STATS")]
        [Tooltip("The base health of this planet.")]
        public int baseHealth;
        
        [Tooltip("The mass below which before the player's planet breaks and downgrades to the planet with the previous size class")]
        public int minMass;
        
        [Tooltip("The mass that needs to be accumulated before the player can move to the next planet " +
                 "(the planet with the next biggest size class.")]
        public int maxMass;
        
        [Tooltip("The rigidbody mass of this planet when the mass is at minMass.")]
        public float minRigidbodyMass;
        
        [Tooltip("The rigidbody mass of this planet when the mass is at maxMass.")]
        public float maxRigidbodyMass;
    }
}