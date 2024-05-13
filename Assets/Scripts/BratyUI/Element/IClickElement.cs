namespace BratyUI.Element
{
    public interface IClickElement
    {
        void HandleClickStart();
        void HandleClickCancel();

        void HandleClickComplete();
    }
}