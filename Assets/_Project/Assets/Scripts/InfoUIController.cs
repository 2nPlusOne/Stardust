using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Spotnose.Stardust
{
    public class InfoUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text totalMassText;
        [SerializeField] private Image massProgressBar;

        private void OnEnable()
        {
            Events.OnMassChanged.AddListener(OnMassChanged);
        }

        private void OnMassChanged(Mass mass)
        {
            totalMassText.text = mass.GetCurrentMass().ToString();
            massProgressBar.fillAmount = mass.GetMassPercentage();
        }
    }
}
