using System;
using DG.Tweening;
using UnityEngine;

namespace Spotnose.Stardust
{
    public class IntroTextScroll : MonoBehaviour
    {
        [SerializeField] float speedMultiplier = 100f;
        private RectTransform _textRect;

        private void OnEnable()
        {
            _textRect = GetComponent<RectTransform>();
            _textRect.anchoredPosition = new Vector2(0, -3000);
        }

        private void Update()
        {
            _textRect.anchoredPosition += new Vector2(0, speedMultiplier * Time.unscaledDeltaTime);
            
            if (_textRect.anchoredPosition.y > 3000 || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                Events.OnIntroScrollFinished.Invoke();
                print("Intro scroll finished");
            }
        }
        
    }
}
