using UnityEngine;

namespace BratyUI
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    public class UINode : MonoBehaviour
    {
        public NodeData NodeData;
        private bool _isDirty;
        
        private void OnValidate()
        {
            _isDirty = true;
        }

        private void Start()
        {
            _isDirty = true;
        }
        
        private void Update()
        {
            if (!_isDirty)
            {
                return;
            }

            DrawNode();
            _isDirty = false;
        }

        private void DrawNode()
        {
            if (transform.parent.TryGetComponent(out NodeBase parentNode))
            {
                var position = ScreenHelper.GetNodePosition(NodeData, parentNode.NodeData);
                transform.position = new Vector3(position.x, position.y, transform.position.z);
            }
        }
    }
}