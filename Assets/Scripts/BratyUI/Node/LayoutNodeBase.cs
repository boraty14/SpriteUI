using UnityEngine;

namespace BratyUI.Node
{
    [DisallowMultipleComponent]
    public abstract class LayoutNodeBase : NodeBase
    {
        protected abstract void SetChildLayouts();

        protected override void DrawCurrentNode()
        {
            SetChildLayouts();
        }

    }
}