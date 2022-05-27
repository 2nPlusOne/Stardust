using Cinemachine;
using UnityEngine;

namespace Spotnose.Stardust
{
    public class SeamlessBackground : MonoBehaviour
    {
        [Range(.1f, 10f)]
        [SerializeField] private float parallax = 1f;
        private Material _material;
        
        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
        }
        
        private void Update()
        {
            var offset = _material.mainTextureOffset;
            
            offset.x = transform.transform.position.x / transform.localScale.x / parallax;
            offset.y = transform.transform.position.y / transform.localScale.y / parallax;
            
            _material.mainTextureOffset = offset;
        }
    }
}
