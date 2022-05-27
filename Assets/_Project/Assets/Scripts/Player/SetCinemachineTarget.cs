using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spotnose.Stardust
{
    public class SetCinemachineTarget : MonoBehaviour
    {
        private void OnEnable()
        {
            Events.OnGameStarted.AddListener(OnGameStart);
        }

        private void OnDisable()
        {
            Events.OnGameStarted.RemoveListener(OnGameStart);
        }
        
        private void OnGameStart(GameObject playerGameObject)
        {
            var cinemachine = GetComponent<Cinemachine.CinemachineVirtualCamera>();
            if (cinemachine == null)
            {
                Debug.LogError("Cinemachine virtual camera component not found");
                return;
            }

            cinemachine.Follow = playerGameObject.transform;
        }
    }
}
