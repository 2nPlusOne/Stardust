using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spotnose.Stardust
{
    public class MenuToggleHandler : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenuFrame;
        [SerializeField] private GameObject upgradeMenuFrame;

        private void OnEnable()
        {
            Events.OnGameStarted.AddListener(OnGameStarted);
            
            Events.OnPauseMenuOpened.AddListener(OnPauseMenuOpened);
            Events.OnPauseMenuClosed.AddListener(OnPauseMenuClosed);
            
            Events.OnUpgradeMenuOpened.AddListener(OnUpgradeMenuOpened);
            Events.OnUpgradeMenuClosed.AddListener(OnUpgradeMenuClosed);
        }

        private void OnDisable()
        {
            Events.OnGameStarted.RemoveListener(OnGameStarted);
            
            Events.OnPauseMenuOpened.RemoveListener(OnPauseMenuOpened);
            Events.OnPauseMenuClosed.RemoveListener(OnPauseMenuClosed);
            
            Events.OnUpgradeMenuOpened.RemoveListener(OnUpgradeMenuOpened);
            Events.OnUpgradeMenuClosed.RemoveListener(OnUpgradeMenuClosed);
        }

        private void OnGameStarted(GameObject playerGameObject)
        {
            //pauseMenuFrame.SetActive(false);
            upgradeMenuFrame.SetActive(false);
        }

        private void OnPauseMenuOpened()
        {
            pauseMenuFrame.SetActive(true);
        }
        
        private void OnPauseMenuClosed()
        {
            pauseMenuFrame.SetActive(false);
        }
        
        private void OnUpgradeMenuOpened()
        {
            upgradeMenuFrame.SetActive(true);
        }
        
        private void OnUpgradeMenuClosed()
        {
            upgradeMenuFrame.SetActive(false);
        }
    }
}
