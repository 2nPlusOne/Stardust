using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Spotnose.Stardust
{
    public class VolumeControl : MonoBehaviour
    {
        [SerializeField] private MixerGroupParameter mixerGroupParameter;
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Slider slider;
        [SerializeField] private float multiplier = 30f;

        private void OnEnable()
        {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
            
            slider.value = PlayerPrefs.GetFloat(mixerGroupParameter.GetString(), mixerGroupParameter.defaultLinearVolume);
        }
        
        private void OnDisable()
        {
            slider.onValueChanged.RemoveListener(OnSliderValueChanged);
            
            PlayerPrefs.SetFloat(mixerGroupParameter.GetString(), slider.value);
            float dB;
            audioMixer.GetFloat(mixerGroupParameter.GetString(), out dB);
            PlayerPrefs.SetFloat(mixerGroupParameter.GetString() + "dB", dB);
        }

        private void Start()
        {
            slider.value = PlayerPrefs.GetFloat(mixerGroupParameter.GetString(), mixerGroupParameter.defaultLinearVolume);
        }

        private void OnSliderValueChanged(float value)
        {
            if (value <= 0) audioMixer.SetFloat(mixerGroupParameter.GetString(), -80f);
            else audioMixer.SetFloat(mixerGroupParameter.GetString(), Mathf.Log10(value) * multiplier);
        }
    }
}
