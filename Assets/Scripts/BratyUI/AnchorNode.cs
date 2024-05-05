using UnityEngine;

namespace BratyUI
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    public class AnchorNode : MonoBehaviour
    {
        [SerializeField] private Vector2 _anchor;
        
        public Vector2 GetAnchor() => _anchor;
        private Vector3 _anchorPosition;

        public void SetAnchorPosition(Vector3 anchorPosition)
        {
            _anchorPosition = anchorPosition;
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

        private void OnValidate()
        {
            _anchor.x = Mathf.Clamp01(_anchor.x);
            _anchor.y = Mathf.Clamp01(_anchor.y);
            name = $"Anchor - X:{_anchor.x} Y:{_anchor.y}";
            if (transform.parent.TryGetComponent(out RootNode rootNode))
            {
                SetAnchorPosition(rootNode.GetAnchorPosition(_anchor));
            }
        }

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