using System;
using UnityEngine;

namespace Spotnose.Stardust
{
    public class UpdateEnginePosition : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        
        private void OnEnable()
        {
            Events.OnMassChanged.AddListener(OnMassChanged);
            Events.OnBodyChanged.AddListener(OnBodyChanged);
            Events.OnEngineChanged.AddListener(OnEngineChanged);
        }
        
        private void OnDisable()
        {
            Events.OnMassChanged.RemoveListener(OnMassChanged);
            Events.OnBodyChanged.RemoveListener(OnBodyChanged);
            Events.OnEngineChanged.RemoveListener(OnEngineChanged);
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void SetSpritePosition(Mass mass)
        {
            var bodyDetails = mass.GetBodyDetails();
            
            var difference = bodyDetails.maxMass - bodyDetails.minMass;
            var massUntilMax = bodyDetails.maxMass - mass.GetCurrentMass();
            var massPercent = 1f - (float) massUntilMax / difference;
            
            var scaleDifference = bodyDetails.maxTransformScale - 1f; 
            var transformScale = 1 + (scaleDifference * massPercent);

            var spriteHeightFactor = _spriteRenderer.sprite.bounds.size.y / 4f;
            print($"Sprite height factor: {spriteHeightFactor}");
            var offset = bodyDetails.engineSpriteBaseYOffset * transformScale - spriteHeightFactor;
            print($"Offset: {offset}");
            gameObject.transform.localPosition = new Vector3(0, offset, 0);
        }

        private void OnMassChanged(Mass mass)
        {

            SetSpritePosition(mass);
        }

        private void OnBodyChanged(BodyDetailsSO bodyDetails, Mass mass)
        {
            SetSpritePosition(mass);
        }

        private void OnEngineChanged(EngineDetailsSO engineDetails)
        {
            var mass = Player.Instance.Mass;
            SetSpritePosition(mass);
        }
    }
}
