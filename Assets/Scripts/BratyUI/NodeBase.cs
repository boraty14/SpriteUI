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
            //transform.position = ScreenHelper.GetNodePosition(NodeData);
        }

        protected void Awake()
        {
            InitializeNode();
        }

        protected virtual void InitializeNode()
        {
            
        }
        
    }
}