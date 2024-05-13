using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BratyUI.Node
{
    [ExecuteAlways]
    [DisallowMultipleComponent]

    public abstract class NodeBase : MonoBehaviour
    {
        [SerializeField] protected NodeCanvas NodeCanvas;
        private bool _isDirty;
        protected bool IsSelected { get; private set; }

        public void SetDirty() => _isDirty = true;

        private void OnValidate()
        {
            _isDirty = true;
            NodeCanvas = transform.GetComponentInParent<NodeCanvas>();
        }

        protected virtual void OnDrawGizmosSelected()
        {
            IsSelected = Selection.activeGameObject == this.gameObject;
            if (!IsSelected)
            {
                return;
            }
            DrawNode();
        }

        protected void Update()
        {
            if (!_isDirty)
            {
                return;
            }
            
            DrawNode();
            _isDirty = false;
        }

        protected abstract void DrawNode();
    }
}