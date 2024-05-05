using UnityEngine;

namespace BratyUI
{
    [ExecuteAlways]
    public class RootNode : MonoBehaviour
    {
        [SerializeField] private NodeCamera _nodeCamera;

        private void OnValidate()
        {
            SetAnchorNodes();
        }

        private void Start()
        {
            SetAnchorNodes();
        }

        private void OnEnable()
        {
            RegisterScreenEvents();
        }

        private void OnDisable()
        {
            UnregisterScreenEvents();
        }
        
        private void SetAnchorNodes()
        {
            var anchors = GetComponentsInChildren<AnchorNode>(true);
            foreach (var anchor in anchors)
            {
                var anchorPosition = GetAnchorPosition(anchor.GetAnchor());
                anchor.SetAnchorPosition(anchorPosition);
            }

        }

        public Vector3 GetAnchorPosition(Vector2 anchor)
        {
            var heightSize = _nodeCamera.Size;
            var widthSize = _nodeCamera.Size * _nodeCamera.Aspect;

            var xPosition = Mathf.Lerp(-widthSize, widthSize, anchor.x);
            var yPosition = Mathf.Lerp(-heightSize, heightSize, anchor.y);

            return new Vector3(xPosition, yPosition, 0f);
        }
        
        private void RegisterScreenEvents()
        {
            ScreenEventDispatcher.OnSafeAreaChange += SetAnchorNodes;
            ScreenEventDispatcher.OnResolutionChange += SetAnchorNodes;
        }
        private void UnregisterScreenEvents()
        {
            ScreenEventDispatcher.OnSafeAreaChange -= SetAnchorNodes;
            ScreenEventDispatcher.OnResolutionChange -= SetAnchorNodes;
        }

    }
}