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
        [Range(0f, 1f)] [SerializeField] private float _horizontalWeight;

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
            _camera.orthographicSize = CalculateCameraSize();
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
            _camera.orthographicSize = CalculateCameraSize();
            OnNodeCameraUpdate?.Invoke();
        }

        private void SetCamera()
        {
            _camera.orthographicSize = CalculateCameraSize();
            OnNodeCameraUpdate?.Invoke();
        }
        
        private float CalculateCameraSize()
        {
            float horizontalSize = (1f / _camera.aspect) * _cameraSize;
            float cameraSize = Mathf.Lerp(_cameraSize, horizontalSize, _horizontalWeight);
            return cameraSize;
        }
    }
}