using System;
using UnityEngine;

namespace BratyUI
{
    [Serializable]
    public class NodeData
    {
        public Vector2 MinAnchor = new Vector2(0.5f, 0.5f);
        public Vector2 MaxAnchor = new Vector2(0.5f, 0.5f);
        public Vector2 Pivot = new Vector2(0.5f, 0.5f);
        public Vector2 Size = Vector2.one;
        public Vector3 Position = Vector3.zero;
    }
}