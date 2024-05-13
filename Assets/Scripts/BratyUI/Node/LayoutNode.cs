using UnityEngine;

namespace BratyUI.Node
{
    [DisallowMultipleComponent]
    public class LayoutNode : NodeBase
    {
        [SerializeField] private Vector2 _position;
        [SerializeField] private Vector2 _sizeOffset;
        [SerializeField] private Vector2 _minAnchor = Vector2.zero;
        [SerializeField] private Vector2 _maxAnchor = Vector2.one;
        [SerializeField] private Vector2 _size;

        protected override void DrawNode()
        {
            var leftBottom = NodeCanvas.GetAnchorPosition(_minAnchor);
            var rightTop = NodeCanvas.GetAnchorPosition(_maxAnchor);

            var size = (Vector2)(rightTop - leftBottom);

            var position = (Vector2)leftBottom + size * 0.5f + _position;
            transform.localPosition = new Vector3(position.x, position.y, leftBottom.z);
            transform.localScale = Vector3.one;

            _size = size + _sizeOffset;
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();

            var localPosition = transform.localPosition;
            
            Vector3 topLeft = new Vector3(localPosition.x - _size.x / 2, localPosition.y + _size.y / 2, 0);
            Vector3 topRight = new Vector3(localPosition.x + _size.x / 2, localPosition.y + _size.y / 2, 0);
            Vector3 bottomRight = new Vector3(localPosition.x + _size.x / 2, localPosition.y - _size.y / 2, 0);
            Vector3 bottomLeft = new Vector3(localPosition.x - _size.x / 2, localPosition.y - _size.y / 2, 0);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
        }
    }
}