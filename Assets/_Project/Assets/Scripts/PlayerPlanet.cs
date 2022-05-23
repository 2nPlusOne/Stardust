using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spotnose
{
    [RequireComponent(typeof(Health), typeof(Mass))]
    [DisallowMultipleComponent]
    public class PlayerPlanet : Singleton<PlayerPlanet>
    {
        public PlanetDetailsSO planetDetails;
        [HideInInspector] public Health health;
        [HideInInspector] public Mass mass;
        [HideInInspector] public Engine currentEngine;

        protected override void Awake()
        {
            base.Awake();
            health = GetComponent<Health>();
            mass = GetComponent<Mass>();
            currentEngine = GetComponentInChildren<Engine>();
            
            Initialize();
        }

        private void Initialize()
        {
            health.SetMaxHealth(planetDetails.baseHealth);
            mass.SetPlanetDetails(planetDetails);
        }
    }
}
