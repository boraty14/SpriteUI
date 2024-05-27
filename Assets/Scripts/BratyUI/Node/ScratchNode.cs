using UnityEngine;

namespace BratyUI.Node
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]

    public class ScratchNode : NodeBase
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

            var position = (Vector2)leftBottom + size * 0.5f + _position;
            transform.localPosition = new Vector3(position.x, position.y, leftBottom.z);
            transform.localScale = Vector3.one;
            
            _spriteRenderer.drawMode = SpriteDrawMode.Sliced;
            _spriteRenderer.size = size + _sizeOffset;
            _collider.size = size + _sizeOffset;
        }
        
#if UNITY_EDITOR
        protected override void DrawSelectedGizmos()
        {
            var localPosition = transform.localPosition;
            var size = _collider.size;

            Vector3 topLeft = new Vector3(localPosition.x - size.x / 2, localPosition.y + size.y / 2, 0);
            Vector3 topRight = new Vector3(localPosition.x + size.x / 2, localPosition.y + size.y / 2, 0);
            Vector3 bottomRight = new Vector3(localPosition.x + size.x / 2, localPosition.y - size.y / 2, 0);
            Vector3 bottomLeft = new Vector3(localPosition.x - size.x / 2, localPosition.y - size.y / 2, 0);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
        }
#endif
    }
}