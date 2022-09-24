using UnityEngine;

namespace Spotnose.Stardust
{
    public class UpdateBodySpriteScale : MonoBehaviour
    {
        private void OnEnable()
        {
            Events.OnMassChanged.AddListener(SetTransformScale);
        }
        
        private void OnDisable()
        {
            Events.OnMassChanged.RemoveListener(SetTransformScale);
        }

        private void SetTransformScale(Mass mass)
        {
            var bodyDetails = mass.GetBodyDetails();
            var difference = bodyDetails.maxMass - bodyDetails.minMass;
            var massUntilMax = bodyDetails.maxMass - mass.GetCurrentMass();
            var massPercent = 1f - (float) massUntilMax / difference;
            
            var scaleDifference = bodyDetails.maxTransformScale - 1f; 
            var transformScale = 1 + (scaleDifference * massPercent);
            transform.localScale = new Vector3(transformScale, transformScale, transformScale);
        }
    }
}
