using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Spotnose.Stardust
{
    [DisallowMultipleComponent]
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] private AudioMixer audioMixer;
        
        // Start is called before the first frame update
        void Start()
        {
            var masterdB = PlayerPrefs.GetFloat(Settings.MasterVolumeParam + "dB", -4f);
            audioMixer.SetFloat(Settings.MasterVolumeParam, masterdB);
            
            var musicdB = PlayerPrefs.GetFloat(Settings.MusicVolumeParam + "dB", -8f);
            audioMixer.SetFloat(Settings.MusicVolumeParam, musicdB);
            
            var sfxdB = PlayerPrefs.GetFloat(Settings.SfxVolumeParam + "dB", -8f);
            audioMixer.SetFloat(Settings.SfxVolumeParam, sfxdB);
        }

        public void PlaySoundEffect(SoundEffectSO soundEffect, float volumeModifier = 1f)
        {
            // Grab a sound effect from the pool
            var sound = (SoundEffect) PoolManager.Instance.GetObject(soundEffect.soundPrefab);
            sound.SetSound(soundEffect);
            sound.gameObject.SetActive(true);
            
            StartCoroutine(DisableSound(sound, soundEffect.audioClip.length));
        }

        private IEnumerator DisableSound(Component sound, float duration)
        {
            yield return new WaitForSeconds(duration);
            sound.gameObject.SetActive(false);
        }
    }
}
