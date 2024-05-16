using UnityEngine;

namespace BratyUI.Element
{
    public abstract class HorizontalScrollElement<TScrollItemElement> : ScrollElementBase<TScrollItemElement>
        where TScrollItemElement : ScrollItemElementBase
    {
        protected override void ScrollView(Vector2 delta)
        {
            View.localPosition += Vector3.right * delta.x;
        }
    }
}