using UnityEngine;

namespace BratyUI.Element.Scroll
{
    public abstract class
        HorizontalScrollElement<TScrollItemElement, TScrollItemModel> : ScrollElementBase<TScrollItemElement,
        TScrollItemModel>
        where TScrollItemElement : ScrollItemElementBase
        where TScrollItemModel : ScrollItemModelBase
    {
        public override void HandleDrag(Vector2 delta)
        {
            //View.localPosition += Vector3.right * delta.x;
        }

        public override void HandleBeginDrag(Vector2 point)
        {
        }

        public override void HandleEndDrag(Vector2 point)
        {
        }
    }
}