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

        public void SetDirty() => _isDirty = true;

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
        
#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            var isSelectedInEditor = Selection.activeGameObject == this.gameObject;
            if (!isSelectedInEditor)
            {
                return;
            }
            
            DrawNode();
            DrawSelectedGizmos();
        }

        protected virtual void DrawSelectedGizmos()
        {
            
        }
        
        private void OnValidate()
        {
            _isDirty = true;
            NodeCanvas = transform.GetComponentInParent<NodeCanvas>(true);
        }
#endif
    }
}