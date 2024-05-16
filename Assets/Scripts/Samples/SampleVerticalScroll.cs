using BratyUI.Element.Scroll;

namespace Samples
{
    public class SampleVerticalScroll : VerticalScrollElement<SampleScrollItemElement, SampleScrollItemModel>
    {
        protected override void InitializeScrollElement()
        {
            base.InitializeScrollElement();
            for (int i = 0; i < 3; i++)
            {
                AddModel(new SampleScrollItemModel
                {
                    DummyData = i 
                });    
            }
        }
    }
}
