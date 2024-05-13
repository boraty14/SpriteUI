using UnityEngine;

namespace BratyUI.Element
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ImageElement : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _collider;
        
        public bool IsBlocking
        {
            get => _collider.enabled;
            set => _collider.enabled = value;
        }
        
        private void OnValidate()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.drawMode = SpriteDrawMode.Sliced;
            _collider = GetComponent<Collider2D>();
        }

    }
}