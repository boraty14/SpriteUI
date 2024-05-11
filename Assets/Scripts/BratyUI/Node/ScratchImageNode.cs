using UnityEngine;

namespace BratyUI.Node
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]

    public class ScratchImageNode : NodeBase
    {
        [SerializeField] private Vector2 _position;
        [SerializeField] private Vector2 _sizeOffset;
        [SerializeField] private Vector2 _minAnchor = Vector2.zero;
        [SerializeField] private Vector2 _maxAnchor = Vector2.one;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private BoxCollider2D _collider;
        
        protected override void DrawNode()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();
            var leftBottom = NodeCanvas.GetAnchorPosition(_minAnchor);
            var rightTop = NodeCanvas.GetAnchorPosition(_maxAnchor);

            var size = (Vector2)(rightTop - leftBottom);
            _spriteRenderer.drawMode = SpriteDrawMode.Sliced;
            _spriteRenderer.size = size + _sizeOffset;

            var position = (Vector2)leftBottom + size * 0.5f + _position;
            transform.localPosition = new Vector3(position.x, position.y, leftBottom.z);
            transform.localScale = Vector3.one;
            
            _collider.size = size + _sizeOffset;
        }
    }
}