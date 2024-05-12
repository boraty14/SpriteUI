using UnityEngine;

namespace BratyUI.Node
{
    [DisallowMultipleComponent]
    public class AnchorNode : NodeBase
    {
        [SerializeField] private Vector2 _anchor;
        
        private Vector3 _anchorPosition;
        
        protected override void DrawNode()
        {
            name = $"Anchor - X:{_anchor.x} Y:{_anchor.y}";
            _anchorPosition = NodeCanvas.GetAnchorPosition(_anchor);
            SetTransform();
        }
        
        private void SetTransform()
        {
            transform.localPosition = _anchorPosition;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

#if UNITY_EDITOR
        private static readonly Color AnchorColor = Color.red;
        private static readonly Color AnchorSelectedColor = Color.green;
        private const float AnchorRadius = 0.3f;

        private void OnDrawGizmos()
        {
            Gizmos.color = AnchorColor;
            Gizmos.DrawWireSphere(transform.position, AnchorRadius);
        }

        protected override void OnDrawGizmosSelected()
        {
            Gizmos.color = AnchorSelectedColor;
            Gizmos.DrawWireSphere(transform.position, AnchorRadius);
            SetTransform();
        }
#endif
        
    }
}