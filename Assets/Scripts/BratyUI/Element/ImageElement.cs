using UnityEngine;

namespace BratyUI.Element
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ImageElement : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _collider;
        
        public bool IsEnabled
        {
            get => _collider.enabled;
            set => _collider.enabled = value;
        }
        
        private void OnValidate()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
        }

    }
}