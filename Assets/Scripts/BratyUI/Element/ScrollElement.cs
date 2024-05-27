using System;
using BratyUI.Element.Gesture;
using BratyUI.Node;
using UnityEngine;

namespace BratyUI.Element
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ScrollElement : MonoBehaviour, IDragElement
    {
        [SerializeField] private ScrollSettings _scrollSettings;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private BoxCollider2D _boxCollider;
        [SerializeField] private ScratchNode _view;
        
        private Vector2 _velocity = Vector2.zero;
        
        public bool IsScrollEnabled
        {
            get => _boxCollider.enabled;
            set => _boxCollider.enabled = value;
        }
        
        public bool IsDragging { get; private set; }

        protected virtual void OnEnable()
        {
            IsDragging = false;
        }
        
        protected virtual void OnDisable()
        {
            IsDragging = false;
        }

        protected virtual void Update()
        {
            if (IsDragging)
            {
                return;
            }
            
            if (_velocity.sqrMagnitude > 0.01f)
            {
                MoveView(_velocity * Time.deltaTime);
                _velocity = Vector2.Lerp(_velocity, Vector2.zero, _scrollSettings.Inertia);
            }
            else
            {
                _velocity = Vector2.zero;
            }
        }

        private void MoveView(Vector2 delta)
        {
            if (_scrollSettings.IsVertical)
            {
                _view.transform.localPosition += Vector3.up * delta.y;
            }

            if (_scrollSettings.IsHorizontal)
            {
                _view.transform.localPosition += Vector3.right * delta.x;
            }
        }

        public void HandleDrag(Vector2 delta)
        {
            _velocity = delta / Time.deltaTime;
            MoveView(delta);
        }

        public void HandleBeginDrag(Vector2 point)
        {
            IsDragging = true;
        }

        public void HandleEndDrag(Vector2 point)
        {
            IsDragging = false;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnDrawGizmosSelected()
        {
            _boxCollider.size = _scrollSettings.Size;
            _spriteRenderer.drawMode = SpriteDrawMode.Sliced;
            _spriteRenderer.size = _scrollSettings.Size;
        }
#endif
    }

    [Serializable]
    public class ScrollSettings
    {
        public Vector2 Size = Vector2.one;
        public float Speed = 1f;
        public float Inertia = 0.15f;
        public bool IsHorizontal;
        public bool IsVertical;
    }
}