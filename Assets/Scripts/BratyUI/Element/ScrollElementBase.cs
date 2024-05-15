using System;
using BratyUI.Element.Gesture;
using UnityEngine;

namespace BratyUI.Element
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class ScrollElementBase<TScrollItemElement> : MonoBehaviour, IDragElement
        where TScrollItemElement : ScrollItemElementBase
    {
        [SerializeField] private TScrollItemElement _scrollItemElement;
        [SerializeField] private ScrollSettings _settings;
        [SerializeField] private BoxCollider2D _collider;

        public bool IsScrollEnabled
        {
            get => _collider.enabled;
            set => _collider.enabled = value;
        }

        private void OnValidate()
        {
            _collider = GetComponent<BoxCollider2D>();
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
        public float StartMargin;
        public float EndMargin;

        [Header("Scroll")]
        public float Speed = 1f;
        public float Inertia = 0.15f;
        public float Space = 0f;
    }
}