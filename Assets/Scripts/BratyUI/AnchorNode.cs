using UnityEngine;

namespace BratyUI
{
    [DisallowMultipleComponent]
    public class AnchorNode : MonoBehaviour, INode
    {
        [SerializeField] private Vector2 _anchor;
        
        private Vector3 _anchorPosition;

        public void DrawNode(RootNode rootNode)
        {
            name = $"Anchor - X:{_anchor.x} Y:{_anchor.y}";
            _anchorPosition = rootNode.GetAnchorPosition(_anchor);
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = AnchorSelectedColor;
            Gizmos.DrawWireSphere(transform.position, AnchorRadius);
            SetTransform();
        }
#endif
    }
}