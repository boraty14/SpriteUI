using UnityEngine;

namespace BratyUI.Node
{
    [ExecuteAlways]
    [DisallowMultipleComponent]

    public abstract class NodeBase : MonoBehaviour
    {
        [SerializeField] protected NodeCanvas NodeCanvas;
        public bool IsDirty { get; protected set; }

        public void SetDirty() => IsDirty = true;

        private void OnValidate()
        {
            IsDirty = true;
            NodeCanvas = transform.GetComponentInParent<NodeCanvas>();
        }

        protected void Update()
        {
            if (!IsDirty)
            {
                return;
            }
            
            DrawNode();
            IsDirty = false;
        }

        protected abstract void DrawNode();
    }
}