using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spotnose.Stardust
{
    public class MenuToggleHandler : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenuFrame;
        [SerializeField] private GameObject pauseMenuRaycastBlocker;
        [SerializeField] private GameObject upgradeMenuFrame;

        [Header("SOUNDS")] 
        [SerializeField] private SoundEffectSO menuOpenSound;
        [SerializeField] private SoundEffectSO menuCloseSound;

        private bool _upgradeWasOpenBeforePause;

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
            pauseMenuFrame.SetActive(true);
            upgradeMenuFrame.SetActive(true);

            pauseMenuRaycastBlocker.SetActive(false);
            pauseMenuFrame.SetActive(false);
            upgradeMenuFrame.SetActive(false);
        }

        private void OnPauseMenuOpened()
        {
            _upgradeWasOpenBeforePause = upgradeMenuFrame.activeSelf;
            upgradeMenuFrame.SetActive(false);
            pauseMenuRaycastBlocker.SetActive(true);
            pauseMenuFrame.SetActive(true);
            SoundManager.Instance.PlaySoundEffect(menuOpenSound);
        }
        
        private void OnPauseMenuClosed()
        {
            pauseMenuRaycastBlocker.SetActive(false);
            pauseMenuFrame.SetActive(false);
            SoundManager.Instance.PlaySoundEffect(menuCloseSound);
            
            if (_upgradeWasOpenBeforePause)
            {
                GameManager.Instance.ChangeState(GameState.UpgradeMenu);
                upgradeMenuFrame.SetActive(true);
            }
        }
        
        private void OnUpgradeMenuOpened()
        {
            upgradeMenuFrame.SetActive(true);
            SoundManager.Instance.PlaySoundEffect(menuOpenSound);
        }
        
        private void OnUpgradeMenuClosed()
        {
            upgradeMenuFrame.SetActive(false);
            SoundManager.Instance.PlaySoundEffect(menuCloseSound);
        }
    }
}
