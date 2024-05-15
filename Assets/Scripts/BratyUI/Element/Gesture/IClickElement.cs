namespace BratyUI.Element.Gesture
{
    public interface IClickElement
    {
        void HandleClickStart();
        void HandleClickCancel();

        void HandleClickComplete();
    }
}