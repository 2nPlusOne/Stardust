using UnityEngine;

namespace Spotnose.Stardust
{
    public class HandleThrusterSFX : MonoBehaviour
    {
        private AudioSource _audioSource;
        private Engine _engine;
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _engine = GetComponent<Engine>();
        }

        private void Start()
        {
            _audioSource.clip = _engine.engineDetails.engineActivationSound;
        }

        private void OnEnable()
        {
            Events.OnThrustInputDown.AddListener(OnThrusterInputDown);
            Events.OnThrustInputUp.AddListener(OnThrusterInputUp);
            Events.OnAfterGameStateChanged.AddListener(OnAfterGameStateChanged);
        }
        
        private void OnDisable()
        {
            Events.OnThrustInputDown.RemoveListener(OnThrusterInputDown);
            Events.OnThrustInputUp.RemoveListener(OnThrusterInputUp);
            Events.OnAfterGameStateChanged.RemoveListener(OnAfterGameStateChanged);
        }

        private void OnAfterGameStateChanged(GameState oldState, GameState newState)
        {
            switch (newState)
            {
                case GameState.Paused or GameState.UpgradeMenu:
                    _audioSource.Pause();
                    break;
                case GameState.Playing:
                    _audioSource.UnPause();
                    break;
                case GameState.MainMenu:
                    _audioSource.Stop();
                    break;
            }
        }

        private void OnThrusterInputDown()
        {
            if (_audioSource.isPlaying) return;
            _audioSource.Play();
        }
        
        private void OnThrusterInputUp()
        {
            if (!_audioSource.isPlaying) return;
            _audioSource.Stop();
        }
        
        void StopThrusting()
        {
            AudioFade(_audioSource, Settings.ThrusterAudioFadeOutTime);
        }

        void AudioFade(AudioSource source, float fadeSpeed)
        {
            if (source.volume > 0)
                source.volume = source.volume - fadeSpeed;
            else
                source.Stop();
        }
    }
}
