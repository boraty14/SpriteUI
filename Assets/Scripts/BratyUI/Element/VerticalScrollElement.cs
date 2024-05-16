using UnityEngine;

namespace BratyUI.Element
{
    public abstract class VerticalScrollElement<TScrollItemElement> : ScrollElementBase<TScrollItemElement>
        where TScrollItemElement : ScrollItemElementBase
    {
        protected override void ScrollView(Vector2 delta)
        {
            View.localPosition += Vector3.up * delta.y;
        }
    }
}