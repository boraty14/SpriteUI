using UnityEngine;

namespace BratyUI.Node
{
    public class HorizontalLayoutNode : LayoutNodeBase
    {
        protected override void SetChildLayouts()
        {
            int childCount = transform.childCount;
            if (childCount == 0)
            {
                return;
            }

            float horizontalSize = Size.x;
            float sizeFactor = horizontalSize / (childCount + 1);
            float startPosition = -horizontalSize * 0.5f;

            for (int i = 0; i < childCount; i++)
            {
                var layoutItem = transform.GetChild(i);
                layoutItem.localPosition = new Vector3(startPosition + sizeFactor * (i + 1), 0f, 0f);
            }
        }
    }
}