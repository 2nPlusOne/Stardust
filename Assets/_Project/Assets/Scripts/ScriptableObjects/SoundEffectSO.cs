using System;
using UnityEngine;
using Random = UnityEngine.Random;

// TODO: Add a list of clips to play and a function to get a random clip

namespace Spotnose
{
    [CreateAssetMenu(fileName = "SoundEffect_", menuName = "Scriptable Objects/Sounds/Sound Effect")]
    public class SoundEffectSO : ScriptableObject
    {
        public string soundEffectName;
        public GameObject soundPrefab;
        public AudioClip audioClip;
        
        [Range(0f, 2f)]
        public float pitch = 1f;
        
        public Vector2 pitchVariationRange = new Vector2(-0.2f, 0.2f);
        
        [Range(0f, 1f)]
        public float volume = 1f;
        
        public Vector2 volumeVariationRange = new Vector2(-0.1f, 0.1f);

        public float GetRandomPitch() => pitch + Random.Range(pitchVariationRange.x, pitchVariationRange.y);
        public float GetRandomVolume() => volume + Random.Range(volumeVariationRange.x, volumeVariationRange.y);
        
        #region Validation
#if UNITY_EDITOR
        private void OnValidate()
        {
            pitch = (float) Math.Round(pitch, 2);
            pitchVariationRange.x = (float) Math.Round(pitchVariationRange.x, 2);
            pitchVariationRange.y = (float) Math.Round(pitchVariationRange.y, 2);
            Math.Clamp(pitchVariationRange.x, -2f, 0f);
            Math.Clamp(pitchVariationRange.y, 0f, 2f);
            volume = (float) Math.Round(volume, 2);
            volumeVariationRange.x = (float) Math.Round(volumeVariationRange.x, 2);
            volumeVariationRange.y = (float) Math.Round(volumeVariationRange.y, 2);
            Math.Clamp(volumeVariationRange.x, -0.25f, 0f);
            Math.Clamp(volumeVariationRange.y, 0f, 0.25f);
        }
#endif
        #endregion
    }
}