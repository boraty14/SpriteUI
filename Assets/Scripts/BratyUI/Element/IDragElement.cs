using UnityEngine;

namespace BratyUI.Element
{
    public interface IDragElement
    {
        void HandleBeginDrag(Vector2 point);
        void HandleEndDrag(Vector2 point);
        void HandleDrag(Vector2 delta);
    }
}