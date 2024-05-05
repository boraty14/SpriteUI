using UnityEngine;

namespace BratyUI
{
    public class RootNode : NodeBase
    {
        [SerializeField] private NodeCamera _nodeCamera;

        private void OnEnable()
        {
            RegisterScreenEvents();
        }

        private void OnDisable()
        {
            UnregisterScreenEvents();
        }

        protected override void DrawNode()
        {
            var baseSize = _nodeCamera.Size * 2f;
            NodeData.Size.x = baseSize * _nodeCamera.Aspect;
            NodeData.Size.y = baseSize;
            
            var childNodes = GetComponentsInChildren<NodeBase>(true);
            foreach (var childNode in childNodes)
            {
                childNode.IsDirty = true;
            }
        }
        
        private void RegisterScreenEvents()
        {
            ScreenEventDispatcher.OnSafeAreaChange += DrawNode;
            ScreenEventDispatcher.OnResolutionChange += DrawNode;
        }
        private void UnregisterScreenEvents()
        {
            ScreenEventDispatcher.OnSafeAreaChange -= DrawNode;
            ScreenEventDispatcher.OnResolutionChange -= DrawNode;
        }

    }
}