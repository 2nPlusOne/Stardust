using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spotnose
{
    public class Engine : MonoBehaviour
    {
        public EngineDetailsSO engineDetails;
        
        public ParticleSystem engineParticles;
        public float currentEngineTurnSpeed;
        public float currentEngineForce;
        public float currentEngineMaxSpeed;

        private void Update()
        {
            currentEngineTurnSpeed = engineDetails.engineTurnSpeed;
            currentEngineForce = engineDetails.engineForce;
            currentEngineMaxSpeed = engineDetails.engineMaxSpeed;
        }
    }
}
