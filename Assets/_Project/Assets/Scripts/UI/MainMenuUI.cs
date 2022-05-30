using System;
using UnityEngine;
using UnityEngine.UI;

namespace Spotnose.Stardust
{
    public class MainMenuUI : MonoBehaviour
    {
        [Header("MAIN MENU PANEL")]
        [Tooltip("The main panel that is a direct child of the main menu canvas.")]
        [SerializeField] private GameObject mainMenuPanel;

        [Header("BUTTONS")]
        [SerializeField] private Button startButton;
        [SerializeField] private Button helpButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button creditsButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button backButton;
        
        [Header("FRAMES")]
        [SerializeField] private GameObject mainMenuFrame;
        [SerializeField] private GameObject helpFrame;
        [SerializeField] private GameObject settingsFrame;
        [SerializeField] private GameObject creditsFrame;
        [SerializeField] private GameObject introFrame;
        
        [Header("SOUNDS")]
        [SerializeField] private SoundEffectSO selectSound;

        private void OnEnable()
        {
            Events.OnMainMenuInputDown.AddListener(OnMainMenuInputDown);
            startButton.onClick.AddListener(OnPlayButtonClicked);
            helpButton.onClick.AddListener(OnHelpButtonClicked);
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            creditsButton.onClick.AddListener(OnCreditsButtonClicked);
            quitButton.onClick.AddListener(OnQuitButtonClicked);
            backButton.onClick.AddListener(OnBackButtonClicked);
            
            Events.OnIntroScrollFinished.AddListener(OnIntroScrollFinished);
        }

        private void OnDisable()
        {
            Events.OnMainMenuInputDown.RemoveListener(OnMainMenuInputDown);
            startButton.onClick.RemoveListener(OnPlayButtonClicked);
            helpButton.onClick.RemoveListener(OnHelpButtonClicked);
            settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            creditsButton.onClick.RemoveListener(OnCreditsButtonClicked);
            quitButton.onClick.RemoveListener(OnQuitButtonClicked);
            backButton.onClick.RemoveListener(OnBackButtonClicked);
            
            Events.OnIntroScrollFinished.RemoveListener(OnIntroScrollFinished);
        }

        private void OnMainMenuInputDown()
        {
            mainMenuPanel.SetActive(true);
            mainMenuFrame.SetActive(true);
            
            helpFrame.SetActive(false);
            settingsFrame.SetActive(false);
            creditsFrame.SetActive(false);
            backButton.gameObject.SetActive(false);
            
            GameManager.Instance.ChangeState(GameState.MainMenu);
        }

        private void OnPlayButtonClicked()
        {
            mainMenuPanel.SetActive(false);
            introFrame.SetActive(true);

            SoundManager.Instance.PlaySoundEffect(selectSound);
        }

        private void OnIntroScrollFinished()
        {
            introFrame.SetActive(false);
            GameManager.Instance.ChangeState(GameState.Starting);
        }

        private void OnHelpButtonClicked()
        {
            mainMenuFrame.SetActive(false);
            helpFrame.SetActive(true);
            backButton.gameObject.SetActive(true);
            
            SoundManager.Instance.PlaySoundEffect(selectSound);
        }

        private void OnSettingsButtonClicked()
        {
            mainMenuFrame.SetActive(false);
            settingsFrame.SetActive(true);
            backButton.gameObject.SetActive(true);
            
            SoundManager.Instance.PlaySoundEffect(selectSound);
        }

        private void OnCreditsButtonClicked()
        {
            mainMenuFrame.SetActive(false);
            creditsFrame.SetActive(true);
            backButton.gameObject.SetActive(true);
            
            SoundManager.Instance.PlaySoundEffect(selectSound);
        }

        private void OnBackButtonClicked()
        {
            mainMenuFrame.SetActive(true);
            helpFrame.SetActive(false);
            settingsFrame.SetActive(false);
            creditsFrame.SetActive(false);
            backButton.gameObject.SetActive(false);
            
            SoundManager.Instance.PlaySoundEffect(selectSound);
        }

        private void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}
