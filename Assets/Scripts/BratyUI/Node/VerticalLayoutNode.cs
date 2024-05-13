using UnityEngine;

namespace BratyUI.Node
{
    public class VerticalLayoutNode : LayoutNodeBase
    {
        protected override void SetChildLayouts()
        {
            int childCount = transform.childCount;
            if (childCount == 0)
            {
                return;
            }

            float verticalSize = Size.y;
            float sizeFactor = verticalSize / (childCount + 1);
            float startPosition = -verticalSize * 0.5f;
            
            for (int i = 0; i < childCount; i++)
            {
                var layoutItem = transform.GetChild(i);
                layoutItem.localPosition = new Vector3(0f, startPosition + sizeFactor * (i + 1), 0f);
            }
        }
    }
}