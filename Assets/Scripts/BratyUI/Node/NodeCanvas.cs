using UnityEngine;

namespace BratyUI.Node
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
            var leftBottom = GetAnchorPosition(Vector2.zero);
            var rightTop = GetAnchorPosition(Vector2.one);
            _collider.size = new Vector2(rightTop.x - leftBottom.x, rightTop.y - leftBottom.y);
            _collider.offset = new Vector2((rightTop.x + leftBottom.x) * 0.5f, (rightTop.y + leftBottom.y) * 0.5f);

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

            var widthStartOffset = _nodeCamera.SafeArea.x / _nodeCamera.ScreenWidth * widthSize * 2f;
            var heightStartOffset = _nodeCamera.SafeArea.y / _nodeCamera.ScreenHeight * heightSize * 2f;
            var xStartPosition = -widthSize + widthStartOffset;
            var yStartPosition = -heightSize + heightStartOffset;

            var widthEndOffset =
                (_nodeCamera.ScreenWidth - _nodeCamera.SafeArea.x - _nodeCamera.SafeArea.width) /
                 _nodeCamera.ScreenWidth * widthSize * 2f;
            var heightEndOffset =
                (_nodeCamera.ScreenHeight - _nodeCamera.SafeArea.y - _nodeCamera.SafeArea.height) /
                _nodeCamera.ScreenHeight * heightSize * 2f;
            var xEndPosition = widthSize - widthEndOffset;
            var yEndPosition = heightSize - heightEndOffset;
            
            var xPosition = xStartPosition + (xEndPosition - xStartPosition) * anchor.x;
            var yPosition = yStartPosition + (yEndPosition - yStartPosition) * anchor.y;

            return new Vector3(xPosition, yPosition, 0.1f);
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