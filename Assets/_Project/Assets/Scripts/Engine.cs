using UnityEngine;

namespace Spotnose.Stardust
{
    public class Engine : MonoBehaviour
    {
        public EngineDetailsSO engineDetails;
        public ParticleSystem engineParticles;
        public float engineTurnSpeed;
        public float engineForce;
        public float engineMaxSpeed;

        private void Update()
        {
            engineTurnSpeed = engineDetails.engineTurnSpeed;
            engineForce = engineDetails.engineForce;
            engineMaxSpeed = engineDetails.engineMaxSpeed;
        }
    }
}
