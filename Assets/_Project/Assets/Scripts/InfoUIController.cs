using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Spotnose.Stardust
{
    public class InfoUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text totalMassText;
        [SerializeField] private TMP_Text metalCountText;
        [SerializeField] private Image massProgressBar;

        private void OnEnable()
        {
            Events.OnMassChanged.AddListener(OnMassChanged);
            Events.OnMetalCountChanged.AddListener(OnMetalChanged);
        }

        private void OnMassChanged(Mass mass)
        {
            totalMassText.text = mass.GetCurrentMass().ToString();
            massProgressBar.fillAmount = mass.GetMassPercentage();
        }
        
        private void OnMetalChanged(int metalCount)
        {
            metalCountText.text = metalCountText.text = metalCount.ToString();
        }
    }
}
