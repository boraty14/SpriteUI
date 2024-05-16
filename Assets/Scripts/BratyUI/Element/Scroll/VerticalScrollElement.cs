using UnityEngine;

namespace BratyUI.Element.Scroll
{
    public abstract class
        VerticalScrollElement<TScrollItemElement, TScrollItemModel> : ScrollElementBase<TScrollItemElement,
        TScrollItemModel>
        where TScrollItemElement : ScrollItemElementBase
        where TScrollItemModel : ScrollItemModelBase
    {
        public override void HandleDrag(Vector2 delta)
        {
            //View.localPosition += Vector3.up * delta.y;
        }

        public override void HandleBeginDrag(Vector2 point)
        {
        }

        public override void HandleEndDrag(Vector2 point)
        {
        }
    }
}