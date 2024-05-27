using System;
using BratyUI.Element.Gesture;
using UnityEngine;

namespace BratyUI.Node
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ScrollNode : NodeBase, IDragElement
    {
        [SerializeField] private ScrollSettings _scrollSettings;
        [SerializeField] private BoxCollider2D _boxCollider;
        [SerializeField] private NodeBase _viewNode;

        private Vector2 _velocity = Vector2.zero;

        public bool IsScrollEnabled
        {
            get => _boxCollider.enabled;
            set => _boxCollider.enabled = value;
        }

        public bool IsDragging { get; private set; }

        protected override void DrawCurrentNode()
        {
            base.DrawCurrentNode();
            _boxCollider.size = TotalSize;
        }

        private void Start()
        {
            SetScrollPercentage(1f);
        }

        public void SetScrollPercentage(float percentage)
        {
            if (_viewNode == null)
            {
                return;
            }

            var localX = 0f;
            var localY = 0f;

            if (_scrollSettings.IsHorizontal)
            {
                float horizontalInterval = -(0.5f * _viewNode.TotalSize.x - 0.5f * TotalSize.x);
                localX = Mathf.Lerp(horizontalInterval, -horizontalInterval, percentage);
            }
            
            if (_scrollSettings.IsVertical)
            {
                float verticalInterval = -(0.5f * _viewNode.TotalSize.y - 0.5f * TotalSize.y);
                localY = Mathf.Lerp(verticalInterval, -verticalInterval, percentage);
            }
            
            _viewNode.UpdatePosition(new Vector2(localX,localY));
        }

        private void OnEnable()
        {
            IsDragging = false;
        }

        private void OnDisable()
        {
            IsDragging = false;
        }

        private void Update()
        {
            if (IsDragging)
            {
                return;
            }

            if (_velocity.sqrMagnitude > 0.01f)
            {
                MoveView(_velocity * Time.deltaTime);
                _velocity = Vector2.Lerp(_velocity, Vector2.zero, _scrollSettings.Inertia * Time.deltaTime);
            }
            else
            {
                _velocity = Vector2.zero;
            }

            if (_viewNode == null)
            {
                return;
            }

            Vector2 position = _viewNode.Position;
            Vector2 targetPosition = position;
            bool isElastic = false;

            float horizontalInterval = (0.5f * _viewNode.TotalSize.x - 0.5f * TotalSize.x);
            if (position.x < -horizontalInterval)
            {
                targetPosition.x = -horizontalInterval;
                isElastic = true;
            }
            else if (position.x > horizontalInterval)
            {
                targetPosition.x = horizontalInterval;
                isElastic = true;
            }

            float verticalInterval = (0.5f * _viewNode.TotalSize.y - 0.5f * TotalSize.y);
            if (position.y < -verticalInterval)
            {
                targetPosition.y = -verticalInterval;
                isElastic = true;
            }
            else if (position.y > verticalInterval)
            {
                targetPosition.y = verticalInterval;
                isElastic = true;
            }

            if (!isElastic)
            {
                return;
            }

            Vector2 displacement = targetPosition - position;
            if (displacement.sqrMagnitude > 0.01f)
            {
                _viewNode.UpdatePosition(_viewNode.Position + (displacement * (_scrollSettings.Elasticity * Time.deltaTime)));
                return;
            }

            _viewNode.UpdatePosition(targetPosition);
        }

        private void MoveView(Vector2 delta)
        {
            if (_viewNode == null)
            {
                return;
            }

            if (_scrollSettings.IsVertical)
            {
                _viewNode.UpdatePosition(_viewNode.Position + (Vector2.up * delta.y));
            }

            if (_scrollSettings.IsHorizontal)
            {
                _viewNode.UpdatePosition(_viewNode.Position + (Vector2.right * delta.x));
            }

            float horizontalInterval = (0.5f * _viewNode.TotalSize.x - 0.5f * TotalSize.x);
            float horizontalPoint = Mathf.Clamp(_viewNode.Position.x,
                -horizontalInterval - _scrollSettings.ScrollOffset.x,
                horizontalInterval + _scrollSettings.ScrollOffset.x);

            float verticalInterval = (0.5f * _viewNode.TotalSize.y - 0.5f * TotalSize.y);
            float verticalPoint = Mathf.Clamp(_viewNode.Position.y,
                -verticalInterval - _scrollSettings.ScrollOffset.y,
                verticalInterval + _scrollSettings.ScrollOffset.y);

            _viewNode.UpdatePosition(new Vector2(horizontalPoint, verticalPoint));
        }

        public void HandleDrag(Vector2 delta)
        {
            delta *= _scrollSettings.Speed;
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
        protected override void OnValidate()
        {
            base.OnValidate();
            _boxCollider = GetComponent<BoxCollider2D>();
        }
#endif
    }

    [Serializable]
    public class ScrollSettings
    {
        public float Speed = 1f;
        public float Inertia = 5f;
        public bool IsHorizontal;
        public bool IsVertical;
        public Vector2 ScrollOffset;
        public float Elasticity = 10f;
    }
}