using System;
using UnityEngine;

namespace BratyUI
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ImageNode : NodeBase
    {
        [SerializeField] private bool _keepNativeSize;
        
        private SpriteRenderer _spriteRenderer;

        public Vector2 Size
        {
            get => _spriteRenderer.size;
            set => _spriteRenderer.size = value;
        }

        protected override void DrawNode()
        {
            base.DrawNode();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.drawMode = SpriteDrawMode.Sliced;
            Size = NodeData.Size;
        }
    }
}
