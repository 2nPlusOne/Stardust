using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spotnose.Stardust
{
    [RequireComponent(typeof(Rigidbody2D))]
    [DisallowMultipleComponent]
    public class BodySizeProgression : MonoBehaviour
    {
        private Rigidbody2D _rb2d;

        private void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            Events.OnMassChanged.AddListener(OnMassChanged);
        }
        
        private void OnDisable()
        {
            Events.OnMassChanged.RemoveListener(OnMassChanged);
        }
        
        private void OnMassChanged(Mass mass)
        {
            SetRigidbodyMass(mass);
            SetTransformScale(mass);
        }

        private void SetRigidbodyMass(Mass mass)
        {
            var bodyDetails = mass.GetBodyDetails();
            var difference = bodyDetails.maxMass - bodyDetails.minMass;
            var massUntilMax = bodyDetails.maxMass - mass.GetCurrentMass();
            var massPercent = 1f - (float) massUntilMax / difference;
            
            var rbMassDifference = bodyDetails.maxRigidbodyMass - bodyDetails.minRigidbodyMass; 
            var rbMass = bodyDetails.minRigidbodyMass + (rbMassDifference * massPercent);
            _rb2d.mass = rbMass;
        }

        private void SetTransformScale(Mass mass)
        {
            var bodyDetails = mass.GetBodyDetails();
            var difference = bodyDetails.maxMass - bodyDetails.minMass;
            var massUntilMax = bodyDetails.maxMass - mass.GetCurrentMass();
            var massPercent = 1f - (float) massUntilMax / difference;
            
            var scaleDifference = bodyDetails.maxTransformScale - 1f; 
            var transformScale = 1 + (scaleDifference * massPercent);
            mass.gameObject.transform.localScale = new Vector3(transformScale, transformScale, transformScale);
        }
    }
}
