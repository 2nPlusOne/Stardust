using System;
using UnityEngine;

namespace Spotnose.Stardust
{
    [DisallowMultipleComponent]
    public class InputHandler : MonoBehaviour
    {
        // Movement input
        private float _turnInput;
        private float _thrustInput;
        private bool _thrustEventTrigger;

        // Update is called once per frame
        void Update()
        {
            _turnInput = Input.GetAxis("Horizontal");
            _thrustInput = Mathf.Clamp01(Input.GetAxis("Vertical"));
            
            // fire an event on the first frame when the thrust input is pressed
            if (_thrustInput > 0 && !_thrustEventTrigger)
            {
                Events.OnThrustInputDown.Invoke();
                _thrustEventTrigger = true;
            }
            
            // fire an event on the first frame when the thrust input is released
            if (_thrustInput == 0 && _thrustEventTrigger)
            {
                Events.OnThrustInputUp.Invoke();
                _thrustEventTrigger = false;
            }

            GetUIInput();
        }

        public float GetTurnInput()
        {
            return _turnInput;
        }
        
        public float GetThrustInput()
        {
            return _thrustInput;
        }
        
        private void GetUIInput()
        {
            // Pause Menu
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) 
                Events.OnPauseMenuInputDown.Invoke();

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Backspace))
                Events.OnPauseMenuInputDown.Invoke();

            // Upgrade Menu
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.U)) 
                Events.OnUpgradeMenuInputDown.Invoke();
        }
    }
}
