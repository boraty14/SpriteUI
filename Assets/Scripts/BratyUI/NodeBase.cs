using UnityEngine;

namespace BratyUI
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    public abstract class NodeBase : MonoBehaviour
    {
        public NodeData NodeData;
        private Vector3 _nodeLocalPosition;
        internal bool IsDirty;
        
        private void OnValidate()
        {
            IsDirty = true;
        }
        
        private void OnDrawGizmosSelected()
        {
            transform.localPosition = _nodeLocalPosition;
        }

        protected virtual void Start()
        {
            IsDirty = true;
        }

        private void Update()
        {
            if (!IsDirty)
            {
                return;
            }

            DrawNode();
            IsDirty = false;
        }
        
        protected virtual void DrawNode()
        {
            if (transform.parent.TryGetComponent(out NodeBase parentNode))
            {
                _nodeLocalPosition = ScreenHelper.GetNodePosition(NodeData, parentNode.NodeData);
                transform.localPosition = _nodeLocalPosition;
            }
        }
    }
}