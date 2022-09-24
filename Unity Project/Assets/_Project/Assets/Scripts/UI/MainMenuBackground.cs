using UnityEngine;
using UnityEngine.UI;

namespace Spotnose.Stardust
{
    public class MainMenuBackground : MonoBehaviour
    {
        [SerializeField] float speedMultiplier = 0.1f;
        private RawImage _rawImage;
        
        private void Awake()
        {
            _rawImage = GetComponent<RawImage>();
        }
        
        private void Update()
        {
            var offset = _rawImage.uvRect.position;
            
            offset.x += Time.unscaledDeltaTime * speedMultiplier;
            offset.y += Time.unscaledDeltaTime * speedMultiplier;
            
            _rawImage.uvRect = new Rect(offset, _rawImage.uvRect.size);
        }
    }
}