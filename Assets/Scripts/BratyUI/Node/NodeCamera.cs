using System;
using BratyUI.Element;
using UnityEngine;

namespace BratyUI.Node
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    [RequireComponent(typeof(ElementRaycaster))]

    public class NodeCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _cameraSize;
        private Resolution _resolution;

        public Rect SafeArea => _resolution.SafeArea;
        public int ScreenWidth => _resolution.Width;
        public int ScreenHeight => _resolution.Height;
        
        public float Aspect => _camera.aspect;
        public float Size => _camera.orthographicSize;

        public event Action OnNodeCameraUpdate;
        
        public Ray GetRayFromPosition(Vector2 mousePosition) => _camera.ScreenPointToRay(mousePosition);

        public Vector3 ScreenToWorldPoint(Vector2 mousePosition) =>
            _camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _camera.nearClipPlane));

        private void OnValidate()
        {
            _camera = GetComponent<Camera>();
            SetCamera();
        }

        private void OnDrawGizmosSelected()
        {
            _camera.orthographicSize = _cameraSize;
        }

        private void OnEnable()
        {
            RegisterScreenEvents();
            SetCamera();
        }

        private void OnDisable()
        {
            UnregisterScreenEvents();
        }

        private void RegisterScreenEvents()
        {
            UnregisterScreenEvents();
            ScreenEventDispatcher.OnResolutionChange += OnResolutionChange;
        }
        
        private void UnregisterScreenEvents()
        {
            ScreenEventDispatcher.OnResolutionChange -= OnResolutionChange;
        }

        private void OnResolutionChange(Resolution resolution)
        {
            _resolution = resolution;
            _camera.orthographicSize = _cameraSize;
            OnNodeCameraUpdate?.Invoke();
        }

        private void SetCamera()
        {
            _camera.orthographicSize = _cameraSize;
            OnNodeCameraUpdate?.Invoke();
        }
    }
}