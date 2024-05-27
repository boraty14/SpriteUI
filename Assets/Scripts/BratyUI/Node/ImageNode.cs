using UnityEngine;

namespace BratyUI.Node
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ImageNode : NodeBase
    {
        [SerializeField] protected SpriteRenderer SpriteRenderer;
        [SerializeField] protected Collider2D ImageCollider;
        
        public bool IsBlocking
        {
            get => ImageCollider != null && ImageCollider.enabled;
            set
            {
                if (ImageCollider != null)
                {
                    ImageCollider.enabled = value;
                }
            }
        }

        protected override void DrawCurrentNode()
        {
            base.DrawCurrentNode();
            SpriteRenderer.size = TotalSize;
        }


#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            SpriteRenderer.drawMode = SpriteDrawMode.Sliced;
            ImageCollider = GetComponent<Collider2D>();
        }
#endif
    }
}