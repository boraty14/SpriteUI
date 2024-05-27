using UnityEngine;

namespace BratyUI.Element.Scroll
{
    public abstract class
        HorizontalScrollElement<TScrollItemModel, TScrollItemElement> : ScrollElementBase<TScrollItemModel,
        TScrollItemElement>
        where TScrollItemModel : ScrollItemModelBase
        where TScrollItemElement : ScrollItemElementBase
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