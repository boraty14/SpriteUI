using System.Collections.Generic;
using BratyUI.Node;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BratyUI.Element
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(NodeCamera))]
    public class ElementRaycaster : MonoBehaviour
    {
        [Header("UI Event Settings")]
        [SerializeField] private float _dragThreshold;

        [Header("Default Settings")] 
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private NodeCamera _nodeCamera;
        
        private readonly RaycastHit2D[] _results = new RaycastHit2D[1];
        private Vector2 _pressPosition;
        private Vector2 _lastPosition;
        private bool _isDragging;
        private RaycastHit2D Result => _results[0];

        private readonly List<IPointerDownElement> _pointerDownElements = new List<IPointerDownElement>();
        private readonly List<IPointerUpElement> _pointerUpElements = new List<IPointerUpElement>();
        private readonly List<IClickElement> _clickElements = new List<IClickElement>();
        private readonly List<IDragElement> _dragElements = new List<IDragElement>();
        private bool _isFirstFrame;

        private void OnValidate()
        {
            _nodeCamera = GetComponent<NodeCamera>();
        }

        private void Start()
        {
            _isFirstFrame = true;
        }

        private void Update()
        {
            if (_isFirstFrame)
            {
                _isFirstFrame = false;
                return;
            }
            
            // pointer down
            if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                var touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                if (!CheckIfInputValid(touchPosition))
                {
                    return;
                }
                
                _pressPosition = _nodeCamera.ScreenToWorldPoint(touchPosition);
                
                if (FireRayAndGetHitCount(touchPosition) == 0)
                {
                    return;
                }
                
                Result.collider.transform.GetComponents<IPointerDownElement>(_pointerDownElements);
                Result.collider.transform.GetComponents<IClickElement>(_clickElements);
                Result.collider.transform.GetComponentsInParent<IDragElement>(true,_dragElements);
                
                foreach (var pointerDownElement in _pointerDownElements)
                {
                    pointerDownElement.HandlePointerDown(_pressPosition);
                }
                _pointerDownElements.Clear();
                
                foreach (var clickElement in _clickElements)
                {
                    clickElement.HandleClickStart();
                }
                return;
            }

            // pointer up
            if (Touchscreen.current.primaryTouch.press.wasReleasedThisFrame)
            {
                var touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                if (!CheckIfInputValid(touchPosition))
                {
                    return;
                }
                
                Vector2 releasePosition = _nodeCamera.ScreenToWorldPoint(touchPosition);
                
                // end drag
                if (_isDragging)
                {
                    foreach (var dragElement in _dragElements)
                    {
                        dragElement.HandleEndDrag(releasePosition);
                    }
                }
                // end click
                else
                {
                    foreach (var clickElement in _clickElements)
                    {
                        clickElement.HandleClickComplete();
                    }
                }

                _isDragging = false;
                _dragElements.Clear();
                _clickElements.Clear();
                
                
                if (FireRayAndGetHitCount(touchPosition) == 0)
                {
                    return;
                }

                Result.collider.transform.GetComponents<IPointerUpElement>(_pointerUpElements);
                foreach (var pointerUpElement in _pointerUpElements)
                {
                    pointerUpElement.HandlePointerUp(releasePosition);
                }
                _pointerUpElements.Clear();

                return;
            }

            // drag
            if (Touchscreen.current.primaryTouch.press.isPressed)
            {
                if (_dragElements.Count == 0)
                {
                    return;
                }
                
                var touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                if (!CheckIfInputValid(touchPosition))
                {
                    return;
                }
                
                Vector2 deltaPosition = _nodeCamera.ScreenToWorldPoint(touchPosition);

                if (!_isDragging)
                {
                    if (Vector2.Distance(deltaPosition, _pressPosition) < _dragThreshold)
                    {
                        return;
                    }
                    
                    foreach (var dragElement in _dragElements)
                    {
                        dragElement.HandleBeginDrag(deltaPosition);
                    }
                    _isDragging = true;
                    _lastPosition = _pressPosition;
                }
                
                var delta = deltaPosition - _lastPosition;
                foreach (var dragElement in _dragElements)
                {
                    dragElement.HandleDrag(delta);
                }

                _lastPosition = deltaPosition;
            }
        }

        private int FireRayAndGetHitCount(Vector2 touchPosition)
        {
            Ray ray = _nodeCamera.GetRayFromPosition(touchPosition);
            int hitCount = Physics2D.RaycastNonAlloc(ray.origin, ray.direction, _results, Mathf.Infinity, _layerMask);
            return hitCount;
        }

        private bool CheckIfInputValid(Vector2 mousePosition)
        {
            return !float.IsInfinity(mousePosition.x) && !float.IsInfinity(mousePosition.y);
        }
    }
}