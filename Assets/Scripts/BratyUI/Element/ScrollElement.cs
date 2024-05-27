using BratyUI.Element.Gesture;
using BratyUI.Node;
using UnityEngine;

namespace BratyUI.Element
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider2D))]
    public class ScrollElement : MonoBehaviour, IDragElement
    {
        [SerializeField] private ScrollSettings _scrollSettings;
        [SerializeField] private BoxCollider2D _boxCollider;
        [SerializeField] private Transform _view;
        
        private Vector2 _velocity = Vector2.zero;
        
        public bool IsScrollEnabled
        {
            get => _boxCollider.enabled;
            set => _boxCollider.enabled = value;
        }
        
        public bool IsDragging { get; private set; }

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
            
            
            Vector3 position = _view.localPosition;
            Vector3 targetPosition = position;
            bool isElastic = false;

            // if (position.x < _scrollSettings.MinScrollPoint.x)
            // {
            //     targetPosition.x = _scrollSettings.MinScrollPoint.x;
            //     isElastic = true;
            // }
            // else if (position.x > _scrollSettings.MaxScrollPoint.x)
            // {
            //     targetPosition.x = _scrollSettings.MaxScrollPoint.x;
            //     isElastic = true;
            // }
            //
            // if (position.y < _scrollSettings.MinScrollPoint.y)
            // {
            //     targetPosition.y = _scrollSettings.MinScrollPoint.y;
            //     isElastic = true;
            // }
            // else if (position.y > _scrollSettings.MaxScrollPoint.y)
            // {
            //     targetPosition.y = _scrollSettings.MaxScrollPoint.y;
            //     isElastic = true;
            // }

            if (!isElastic)
            {
                return;
            }
            
            Vector3 displacement = targetPosition - position;
            if (displacement.sqrMagnitude > 0.01f)
            {
                _view.localPosition += displacement * (_scrollSettings.Elasticity * Time.deltaTime);
                return;
            }

            _view.localPosition = targetPosition;
        }

        private void MoveView(Vector2 delta)
        {
            if (_scrollSettings.IsVertical)
            {
                _view.localPosition += Vector3.up * delta.y;
            }

            if (_scrollSettings.IsHorizontal)
            {
                _view.localPosition += Vector3.right * delta.x;
            }
            
            // float horizontalPoint = Mathf.Clamp(_view.localPosition.x, _scrollSettings.MinScrollPoint.x - _scrollSettings.ScrollOffset.x,
            //     _scrollSettings.MaxScrollPoint.x + _scrollSettings.ScrollOffset.x);
            //
            // float verticalPoint = Mathf.Clamp(_view.localPosition.y, _scrollSettings.MinScrollPoint.y - _scrollSettings.ScrollOffset.y,
            //     _scrollSettings.MaxScrollPoint.y + _scrollSettings.ScrollOffset.y);
            
            //_view.localPosition = new Vector3(horizontalPoint, verticalPoint, _view.localPosition.z);
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

        private void OnValidate()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
        }
    }

    
}