using System;
using UnityEngine;

namespace BratyUI
{
    [Serializable]
    public class NodeAnchor
    {
        public Vector2 MinAnchor = new Vector2(0.5f, 0.5f);
        public Vector2 MaxAnchor = new Vector2(0.5f, 0.5f);
        public Vector2 MinAnchorOffset = new Vector2(0f, 0f);
        public Vector2 MaxAnchorOffset = new Vector2(0f, 0f);
    }
    
    
}