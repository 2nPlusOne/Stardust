using UnityEngine;
using UnityEngine.UI;

namespace Spotnose.Stardust
{
    public class PauseMenuUIController : MonoBehaviour
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private GameObject _pauseMenuSettingsFrame;
        [SerializeField] private Button _pauseMenuSettingsBackButton;
        
        [Header("Sounds")]
        [SerializeField] private SoundEffectSO selectSound;

        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(OnResumeButtonClicked);
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            _pauseMenuSettingsBackButton.onClick.AddListener(OnPauseMenuSettingsBackButtonClicked);
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
            _quitButton.onClick.AddListener(OnQuitButtonClicked);
            
            _pauseMenuSettingsFrame.SetActive(false);
            _resumeButton.gameObject.SetActive(true);
            _settingsButton.gameObject.SetActive(true);
            _restartButton.gameObject.SetActive(true);
            _mainMenuButton.gameObject.SetActive(true);
            _quitButton.gameObject.SetActive(true);
        }
        
        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            _pauseMenuSettingsBackButton.onClick.RemoveListener(OnPauseMenuSettingsBackButtonClicked);
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
            _quitButton.onClick.RemoveListener(OnQuitButtonClicked);
        }

        private void OnResumeButtonClicked()
        {
            Events.OnPauseMenuInputDown.Invoke();
            SoundManager.Instance.PlaySoundEffect(selectSound);
        }

        private void OnSettingsButtonClicked()
        {
            _resumeButton.gameObject.SetActive(false);
            _settingsButton.gameObject.SetActive(false);
            _restartButton.gameObject.SetActive(false);
            _mainMenuButton.gameObject.SetActive(false);
            _quitButton.gameObject.SetActive(false);
            
            _pauseMenuSettingsFrame.SetActive(true);
            SoundManager.Instance.PlaySoundEffect(selectSound);
        }

        private void OnPauseMenuSettingsBackButtonClicked()
        {
            _pauseMenuSettingsFrame.SetActive(false);
            
            _resumeButton.gameObject.SetActive(true);
            _settingsButton.gameObject.SetActive(true);
            _restartButton.gameObject.SetActive(true);
            _mainMenuButton.gameObject.SetActive(true);
            _quitButton.gameObject.SetActive(true);
            
            SoundManager.Instance.PlaySoundEffect(selectSound);
        }

        private void OnRestartButtonClicked()
        {
            GameManager.Instance.ChangeState(GameState.Starting);
            SoundManager.Instance.PlaySoundEffect(selectSound);
        }

        private void OnMainMenuButtonClicked()
        {
            Events.OnMainMenuInputDown.Invoke();
            SoundManager.Instance.PlaySoundEffect(selectSound);
        }

        private void OnQuitButtonClicked()
        {
            print("Quitting...");
            Application.Quit();
        }
    }
}
