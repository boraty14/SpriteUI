using System;
using UnityEngine;

namespace BratyUI
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider2D))]
    public class NodeCanvas : MonoBehaviour
    {
        [SerializeField] private NodeCamera _nodeCamera;
        [SerializeField] private BoxCollider2D _collider;

        public bool IsBlocking
        {
            get => _collider.enabled;
            set => _collider.enabled = value;
        }

        private void OnValidate()
        {
            DrawNodes();
        }

        private void OnEnable()
        {
            DrawNodes();
            RegisterCameraEvents();
        }

        private void OnDisable()
        {
            UnregisterCameraEvents();
        }
        
        private void DrawNodes()
        {
            if (_nodeCamera == null)
            {
                return;
            }
            
            _collider = GetComponent<BoxCollider2D>();
            _collider.size = new Vector2(_nodeCamera.Size * _nodeCamera.Aspect * 2f, _nodeCamera.Size * 2f);
            
            var nodes = GetComponentsInChildren<NodeBase>(true);
            foreach (var node in nodes)
            {
                node.SetDirty();
            }
        }

        public Vector3 GetAnchorPosition(Vector2 anchor)
        {
            var heightSize = _nodeCamera.Size;
            var widthSize = _nodeCamera.Size * _nodeCamera.Aspect;

            var xPosition = -widthSize + (2 * widthSize * anchor.x);
            var yPosition = -heightSize + (2 * heightSize * anchor.y);

            return new Vector3(xPosition, yPosition, 0f);
        }
        
        private void RegisterCameraEvents()
        {
            if (_nodeCamera == null)
            {
                return;
            }
            
            UnregisterCameraEvents();
            _nodeCamera.OnNodeCameraUpdate += DrawNodes;
        }
        private void UnregisterCameraEvents()
        {
            if (_nodeCamera == null)
            {
                return;
            }
            
            _nodeCamera.OnNodeCameraUpdate -= DrawNodes;
        }
    }
}