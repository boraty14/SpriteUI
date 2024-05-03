using UnityEngine;

namespace BratyUI
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    public abstract class NodeBase : MonoBehaviour
    {
        public NodeData NodeData;
        protected bool IsDirty;
        
        private void OnValidate()
        {
            IsDirty = true;
        }

        protected virtual void Awake()
        {
            IsDirty = true;
        }

        protected virtual void OnDestroy()
        {
            UnregisterScreenEvents();
        }

        private void Update()
        {
            if (!IsDirty)
            {
                return;
            }

            UnregisterScreenEvents();
            RegisterScreenEvents();
            DrawNode();
            IsDirty = false;
        }
        
        protected virtual void DrawNode()
        {
            var rootNode = GetComponentInParent<RootNode>();
            var position = ScreenHelper.GetNodePosition(NodeData, rootNode.NodeCamera);
            transform.position = new Vector3(position.x, position.y, transform.position.z);
        }

        private void RegisterScreenEvents()
        {
            ScreenEventDispatcher.OnSafeAreaChange += OnSafeAreaChange;
            ScreenEventDispatcher.OnResolutionChange += OnResolutionChange;
        }
        private void UnregisterScreenEvents()
        {
            ScreenEventDispatcher.OnSafeAreaChange -= OnSafeAreaChange;
            ScreenEventDispatcher.OnResolutionChange -= OnResolutionChange;
        }

        private void OnResolutionChange()
        {
            IsDirty = true;
        }

        private void OnSafeAreaChange()
        {
            IsDirty = true;
        }
    }
}