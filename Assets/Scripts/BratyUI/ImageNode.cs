using UnityEngine;

namespace BratyUI
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ImageNode : NodeBase
    {
        private SpriteRenderer _spriteRenderer;
        
        protected override void InitializeNode()
        {
            base.InitializeNode();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
