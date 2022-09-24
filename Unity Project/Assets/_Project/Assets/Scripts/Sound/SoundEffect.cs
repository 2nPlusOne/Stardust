using UnityEngine;
using UnityEngine.Audio;

namespace Spotnose.Stardust
{
    [RequireComponent(typeof(AudioSource))]
    [DisallowMultipleComponent]
    public class SoundEffect : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup outputMixerGroup;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.outputAudioMixerGroup = outputMixerGroup;
        }

        private void OnEnable()
        {
            if (_audioSource.clip != null)
                _audioSource.Play();
        }
        
        private void OnDisable()
        {
            _audioSource.Stop();
        }

        public void SetSound(SoundEffectSO soundEffect, float volumeModifier = 1f)
        {
            _audioSource.pitch = soundEffect.GetRandomPitch();
            _audioSource.volume = soundEffect.GetRandomVolume() * volumeModifier;
            _audioSource.clip = soundEffect.audioClip;
        }
    }
}