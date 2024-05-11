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

        [Header("Default Settings")] 
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private NodeCamera _nodeCamera;
        
        private readonly RaycastHit2D[] _results = new RaycastHit2D[1];
        private Vector2 _lastPosition;
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
            
            Debug.Log(Touchscreen.current);
            var position = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector2 worldPosition = _nodeCamera.ScreenToWorldPoint(position);
            
            // pointer down
            if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                if (FireRayAndGetHitCount() == 0)
                {
                    return;
                }

                Result.collider.transform.GetComponents<IPointerDownElement>(_pointerDownElements);
                Result.collider.transform.GetComponents<IDragElement>(_dragElements);
                foreach (var pointerDownElement in _pointerDownElements)
                {
                    pointerDownElement.HandlePointerDown(worldPosition);
                }
                return;
            }

            // pointer up
            if (Touchscreen.current.primaryTouch.press.wasReleasedThisFrame)
            {
                _dragElements.Clear();
                if (FireRayAndGetHitCount() == 0)
                {
                    return;
                }

                Result.collider.transform.GetComponents<IPointerUpElement>(_pointerUpElements);
                foreach (var pointerUpElement in _pointerUpElements)
                {
                    pointerUpElement.HandlePointerUp(worldPosition);
                }
                
                // click
                
                Result.collider.transform.GetComponents<IClickElement>(_clickElements);
                foreach (var clickElement in _clickElements)
                {
                    clickElement.HandleClick();
                }
                
                return;
            }

            // drag
            if (Touchscreen.current.primaryTouch.press.isPressed)
            {
                if (_dragElements.Count == 0)
                {
                    return;
                }
                
                var delta = worldPosition - _lastPosition;
                foreach (var dragElement in _dragElements)
                {
                    dragElement.HandleDrag(delta);
                }
            }
        }

        private int FireRayAndGetHitCount()
        {
            Vector2 mousePosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Ray ray = _nodeCamera.GetRayFromPosition(mousePosition);
            int hitCount = Physics2D.RaycastNonAlloc(ray.origin, ray.direction, _results, Mathf.Infinity, _layerMask);
            return hitCount;
        }
    }
}