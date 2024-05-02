using UnityEngine;

namespace BratyUI
{
    [DisallowMultipleComponent]
    public abstract class NodeBase : MonoBehaviour
    {
        public NodeData NodeData;
        
        protected virtual void OnValidate()
        {
            InitializeNode();
        }

        protected virtual void Awake()
        {
            InitializeNode();
            ScreenEventDispatcher.OnSafeAreaChange += OnSafeAreaChange;
            ScreenEventDispatcher.OnResolutionChange += OnResolutionChange;
        }

        protected virtual void OnDestroy()
        {
            ScreenEventDispatcher.OnSafeAreaChange -= OnSafeAreaChange;
            ScreenEventDispatcher.OnResolutionChange -= OnResolutionChange;
        }

        private void OnResolutionChange()
        {
            InitializeNode();
        }

        private void OnSafeAreaChange()
        {
            InitializeNode();
        }

        protected virtual void InitializeNode()
        {
            var nodeCamera = GetComponentInParent<RootNode>().NodeCamera;
            var position = ScreenHelper.GetNodePosition(NodeData, nodeCamera);
            transform.position = new Vector3(position.x, position.y, transform.position.z);
        }
        
    }
}