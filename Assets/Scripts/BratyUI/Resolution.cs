﻿using System;
using UnityEngine;

namespace BratyUI
{
    [Serializable]
    public struct Resolution
    {
        public int Width;
        public int Height;
        public Rect SafeArea;

        public override string ToString()
        {
            return $"Width: {Width} Height: {Height} SafeArea: {SafeArea}";
        }
    }
}