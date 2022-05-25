using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Spotnose.Stardust
{
    public class InfoUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text typeText;
        [SerializeField] private TMP_Text sizeText;
        [SerializeField] private Image bodySprite;
        
        [SerializeField] private TMP_Text totalMassText;
        [SerializeField] private TMP_Text metalCountText;
        [SerializeField] private Button engineUpgradeButton;
        [SerializeField] private TMP_Text nextSizeProgressionText;
        [SerializeField] private Image massProgressBar;
        
        private Coroutine _progressBarLerpCoroutine;

        private void OnEnable()
        {
            Events.OnMassChanged.AddListener(OnMassChanged);
            Events.OnBodyChanged.AddListener(OnBodyChanged);
            Events.OnMetalCountChanged.AddListener(OnMetalChanged);
        }

        private void OnDisable()
        {
            Events.OnMassChanged.RemoveListener(OnMassChanged);
            Events.OnBodyChanged.RemoveListener(OnBodyChanged);
            Events.OnMetalCountChanged.RemoveListener(OnMetalChanged);
        }

        private void OnMassChanged(Mass mass)
        {
            totalMassText.text = mass.GetCurrentMass().ToString();

            LerpProgressBar(mass);
        }

        private void OnBodyChanged(BodyDetailsSO bodyDetails, Mass mass)
        {
            massProgressBar.fillAmount = mass.GetMassPercentage();
            typeText.text = bodyDetails.celestialBodyType.ToString();
            sizeText.text = bodyDetails.sizeCategory.ToString();
            bodySprite.sprite = bodyDetails.sprite;

            var nextBody = bodyDetails.nextBodyDetails;
            var nextText = $"TO {nextBody.sizeCategory.ToString()} {nextBody.celestialBodyType.ToString()}";
            nextSizeProgressionText.text = nextText;
        }

        private void OnMetalChanged(int metalCount)
        {
            metalCountText.text = metalCountText.text = metalCount.ToString();
        }

        private void LerpProgressBar(Mass mass)
        {
            var lerpRoutine = LerpProgressBarRoutine(mass.GetMassPercentage(), 1f);
            if (_progressBarLerpCoroutine != null)
            {
                StopCoroutine(_progressBarLerpCoroutine);
                _progressBarLerpCoroutine = StartCoroutine(lerpRoutine);
                return;
            }
            
            _progressBarLerpCoroutine = StartCoroutine(lerpRoutine);
        }

        private IEnumerator LerpProgressBarRoutine(float endValue, float duration)
        {
            var startValue = massProgressBar.fillAmount;
            var t = 0f;
            while (t < duration)
            {
                t += Time.deltaTime;
                massProgressBar.fillAmount = Mathf.Lerp(startValue, endValue, t / duration);
                yield return null;
            }
        }
    }
}
