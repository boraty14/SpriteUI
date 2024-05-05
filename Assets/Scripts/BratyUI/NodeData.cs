using System;
using UnityEngine;

namespace BratyUI
{
    [Serializable]
    public class NodeData
    {
        public NodeAnchor NodeAnchor;
        public Vector2 Size = Vector2.one;
        public Vector3 Position = Vector3.zero;
    }
}