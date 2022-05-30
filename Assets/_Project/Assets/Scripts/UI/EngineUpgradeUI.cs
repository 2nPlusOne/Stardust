using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Spotnose.Stardust
{
    public class EngineUpgradeUI : MonoBehaviour
    {
        [SerializeField] private EngineDetailsSO engineDetails;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Image engineImageIcon;
        [SerializeField] private Image costButtonImage;
        [SerializeField] private Button costButton;
        [SerializeField] private TMP_Text requiresText;
        [SerializeField] private TMP_Text costText;
        [SerializeField] private TMP_Text costTypeText;
        
        [Header("SOUNDS")]
        [SerializeField] private SoundEffectSO engineUpgradeSound;
        
        private bool _canAfford;
        private bool _upgraded;
        private bool _locked;
        
        private const string LockedText = "Locked";
        private const string UpgradedText = "Unlocked";
        private const float UpgradedTextFontSize = 6f;
        private const float LockedTextFontSize = 6f;
        private const float CostTextFontSize = 16f;

        private const float AvailableSelectedAlpha = 0.4f;
        private const float AvailableUnselectedAlpha = 0.25f;
        //private readonly Color _unlockedColor = new(0f, 0.7843f, 0.02f, AvailableSelectedAlpha);
        private readonly Color _availableSelectedColor = new(0f, 0.7843f, 0.02f, AvailableSelectedAlpha);
        private readonly Color _availableDeselectedColor = new(0f, 0.7843f, 0.02f, AvailableUnselectedAlpha);
        
        private const float UnavailableSelectedAlpha = 0.45f;
        private const float UnavailableDeselectedAlpha = 0.3f;
        private Color _unavailableSelectedColor = new(1f, 0f, 0f, UnavailableSelectedAlpha);
        private readonly Color _unavailableDeselectedColor = new(1f, 0f, 0f, UnavailableDeselectedAlpha);

        private void OnEnable()
        {
            costButton.onClick.AddListener(OnCostButtonClicked);
            Events.OnEngineChanged.AddListener(OnEngineChanged);

            UpdateUI();
        }
        
        private void OnDisable()
        {
            costButton.onClick.RemoveListener(OnCostButtonClicked);
            Events.OnEngineChanged.RemoveListener(OnEngineChanged);

            UpdateUI();
        }

        private void Awake()
        {
            var startingEngine = Player.Instance.startingLoadout.startingEngineDetails;
            if (engineDetails.engineUpgradeLevel == startingEngine.engineUpgradeLevel)
            {
                SetToUpgraded();
                return;
            }
            
            nameText.text = engineDetails.engineName;
            engineImageIcon.sprite = engineDetails.engineIcon;
            costText.text = engineDetails.upgradeCost.ToString();
            costTypeText.text = engineDetails.upgradeRequiresItemType.ToString();
        }

        private void UpdateUI()
        {
            nameText.text = engineDetails.engineName;
            engineImageIcon.sprite = engineDetails.engineIcon;
            
            var player = Player.Instance;
            
            if (player is null) return;
            if (_upgraded) return;
            
            _locked = engineDetails.engineUpgradeLevel > player.CurrentEngine.engineDetails.engineUpgradeLevel + 1;
            if (_locked)
            {
                SetToLocked();
                return;
            }

            SetToUnlocked();
        }

        private void SetToUnlocked()
        {
            var player = Player.Instance;
            
            costButton.interactable = true;
            requiresText.enabled = true;
            costText.fontSize = CostTextFontSize;
            costText.text = engineDetails.upgradeCost.ToString();
            costTypeText.enabled = true;
            costTypeText.text = engineDetails.upgradeRequiresItemType.ToString();
            
            _canAfford = player.Inventory.GetItemCount(engineDetails.upgradeRequiresItemType) >= engineDetails.upgradeCost;

            costButtonImage.color = _canAfford ? _availableDeselectedColor : _unavailableDeselectedColor;
        }

        // set to locked

        private void SetToLocked()
        {
            _locked = true;
            
            costButtonImage.color = _unavailableDeselectedColor;
            costButton.interactable = false;
            requiresText.enabled = false;
            costText.fontSize = LockedTextFontSize;
            costText.text = LockedText;
            costTypeText.enabled = false;
        }

        private void SetToUpgraded()
        {
            _upgraded = true;
            
            costButtonImage.color = _availableSelectedColor;
            costButton.interactable = false;
            requiresText.enabled = false;
            costText.fontSize = UpgradedTextFontSize;
            costText.text = UpgradedText;
            costTypeText.enabled = false;
        }

        private void OnCostButtonClicked()
        {
            var player = Player.Instance;
            if (!_canAfford)
            {
                print("Not enough " + engineDetails.upgradeRequiresItemType + " to upgrade");
                SoundManager.Instance.PlaySoundEffect(engineUpgradeSound);
                return;
            }
            player.Inventory.RemoveItem(engineDetails.upgradeRequiresItemType, engineDetails.upgradeCost);
            SetToUpgraded();
            Events.OnEngineUpgradePurchased.Invoke(engineDetails);
        }

        private void OnEngineChanged(EngineDetailsSO obj) => UpdateUI();
    }
}
