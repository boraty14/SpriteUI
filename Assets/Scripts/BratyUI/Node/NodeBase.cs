using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BratyUI.Node
{
    [DisallowMultipleComponent]
    public abstract class NodeBase : MonoBehaviour
    {
        [SerializeField] private NodeTransform _nodeTransform = new();
        [SerializeField] private NodeCanvas _nodeCanvas;
        [SerializeField] private NodeBase _parentNode;
        private bool _isDirty;

        public Vector2 Position => _nodeTransform.Position;
        public Vector2 TotalSize { get; private set; }

        public void UpdatePosition(Vector2 position)
        {
            _nodeTransform.Position = position;
            transform.localPosition = new Vector3(position.x, position.y, transform.localPosition.z);
        }

        private void LateUpdate()
        {
            if (!_isDirty)
            {
                return;
            }

            DrawCurrentNode();
            _isDirty = false;
        }

        public Vector3 GetAnchorPosition(Vector2 anchor)
        {
            if (_parentNode != null)
            {
                var parentSize = _parentNode.TotalSize;
                return new Vector3(-parentSize.x * 0.5f + (anchor.x * parentSize.x),
                    -parentSize.y * 0.5f + (anchor.y * parentSize.y), transform.localPosition.z);
            }

            return _nodeCanvas.GetAnchorPosition(anchor);
        }

        public void DrawNode()
        {
            var leftBottom = GetAnchorPosition(_nodeTransform.MinAnchor);
            var rightTop = GetAnchorPosition(_nodeTransform.MaxAnchor);

            var size = (Vector2)(rightTop - leftBottom);

            var position = (Vector2)leftBottom + size * 0.5f + _nodeTransform.Position;
            transform.localPosition = new Vector3(position.x, position.y, leftBottom.z);
            transform.localScale = Vector3.one;

            TotalSize = size + _nodeTransform.SizeOffset;
            _isDirty = true;

            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out NodeBase childNode))
                {
                    childNode.DrawNode();
                }
            }

#if UNITY_EDITOR
            EditorApplication.delayCall -= DrawCurrentNode;
            EditorApplication.delayCall += DrawCurrentNode;
#endif
        }

        protected virtual void DrawCurrentNode()
        {
            _isDirty = false;
#if UNITY_EDITOR
            EditorApplication.delayCall -= DrawCurrentNode;
#endif
        }

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            var isSelectedInEditor = Selection.activeGameObject == this.gameObject;
            if (!isSelectedInEditor)
            {
                return;
            }

            DrawNode();

            var localPosition = transform.localPosition;

            Vector3 topLeft = new Vector3(localPosition.x - TotalSize.x / 2, localPosition.y + TotalSize.y / 2, 0);
            Vector3 topRight = new Vector3(localPosition.x + TotalSize.x / 2, localPosition.y + TotalSize.y / 2, 0);
            Vector3 bottomRight = new Vector3(localPosition.x + TotalSize.x / 2, localPosition.y - TotalSize.y / 2, 0);
            Vector3 bottomLeft = new Vector3(localPosition.x - TotalSize.x / 2, localPosition.y - TotalSize.y / 2, 0);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
        }

        protected virtual void OnValidate()
        {
            _nodeCanvas = transform.GetComponentInParent<NodeCanvas>(true);
            _parentNode = transform.parent.GetComponent<NodeBase>();
            DrawNode();
        }
#endif
    }

    [Serializable]
    public class NodeTransform
    {
        public Vector2 Position;
        public Vector2 SizeOffset;
        public Vector2 MinAnchor = new Vector2(0.5f, 0.5f);
        public Vector2 MaxAnchor = new Vector2(0.5f, 0.5f);
    }
}