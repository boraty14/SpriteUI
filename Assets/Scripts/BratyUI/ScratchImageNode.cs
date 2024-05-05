using UnityEngine;

namespace BratyUI
{
    public class ScratchImageNode : MonoBehaviour, INode
    {
        [SerializeField] private Vector2 _position;
        [SerializeField] private Vector2 _size;
        [SerializeField] private Vector2 _minAnchor;
        [SerializeField] private Vector2 _maxAnchor;
        private SpriteRenderer _spriteRenderer;

        public void DrawNode(RootNode rootNode)
        {
            var leftBottom = rootNode.GetAnchorPosition(_minAnchor);
            var rightTop = rootNode.GetAnchorPosition(_maxAnchor);
            
            
        }

        // private void SetSpriteRenderer()
        // {
        //     _spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        //     _spriteRenderer.size = 
        // }
        //
        // private Vector2 GetSpriteSize()
        // {
        //     var rootNode = transform.GetComponentInParent<RootNode>();
        // }

        
    }
}