using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Spotnose.Stardust
{
    public class InvertSteeringControl : MonoBehaviour
    {
        [SerializeField] private Toggle invertSteeringToggle;

        private void OnEnable()
        {
            invertSteeringToggle.onValueChanged.AddListener(OnInvertSteeringToggleValueChanged);
        }
        
        private void OnDisable()
        {
            invertSteeringToggle.onValueChanged.RemoveListener(OnInvertSteeringToggleValueChanged);
        }

        private void Start()
        {
            invertSteeringToggle.isOn = PlayerPrefs.GetInt("InvertSteering", 0) == 1;
        }

        private void OnInvertSteeringToggleValueChanged(bool isOn)
        {
            PlayerPrefs.SetInt("InvertSteering", invertSteeringToggle.isOn ? 1 : 0);
            Events.OnInvertSteeringChanged.Invoke(invertSteeringToggle.isOn);
            print($"Invoking OnInvertSteeringChanged with {invertSteeringToggle.isOn}");
        }
    }
}
