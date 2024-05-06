using UnityEngine;

namespace BratyUI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]

    public class ScratchImageNode : NodeBase
    {
        [SerializeField] private Vector2 _position;
        [SerializeField] private Vector2 _size;
        [SerializeField] private Vector2 _minAnchor;
        [SerializeField] private Vector2 _maxAnchor;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private BoxCollider2D _collider;
        
        protected override void DrawNode()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();
            var leftBottom = NodeCanvas.GetAnchorPosition(_minAnchor);
            var rightTop = NodeCanvas.GetAnchorPosition(_maxAnchor);
            _collider.size = Vector2.one;
            _spriteRenderer.drawMode = SpriteDrawMode.Sliced;
            _spriteRenderer.size = Vector2.one;
        }
    }
}