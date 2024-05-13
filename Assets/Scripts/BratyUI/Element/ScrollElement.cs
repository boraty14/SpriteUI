using System;
using UnityEngine;

namespace BratyUI.Element
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class ScrollElement : MonoBehaviour, IDragElement
    {
        [SerializeField] private ScrollSettings _settings;
        [SerializeField] private BoxCollider2D _boxCollider2D;

        private void OnValidate()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }


        public void HandleDrag(Vector2 delta)
        {
        }

        public void HandleBeginDrag(Vector2 point)
        {
        }

        public void HandleEndDrag(Vector2 point)
        {
        }
    }

    [Serializable]
    public class ScrollSettings
    {
        [Header("Margin")]
        public float TopMargin;
        public float BottomMargin;
        public float LeftMargin;
        public float RightMargin;

        [Header("Scroll")]
        public bool IsHorizontal;
        public float Speed = 1f;
        public float Inertia = 0.15f;
    }
}