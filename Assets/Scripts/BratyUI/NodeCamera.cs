using System;
using UnityEngine;

namespace BratyUI
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public class NodeCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _cameraSize;
        [Range(0f, 1f)] [SerializeField] private float _horizontalWeight;
        
        public float Aspect => _camera.aspect;
        public float Size => _camera.orthographicSize;

        public event Action OnNodeCameraUpdate;

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
            ScreenEventDispatcher.OnSafeAreaChange += SetCamera;
            ScreenEventDispatcher.OnResolutionChange += SetCamera;
        }
        
        private void UnregisterScreenEvents()
        {
            ScreenEventDispatcher.OnSafeAreaChange -= SetCamera;
            ScreenEventDispatcher.OnResolutionChange -= SetCamera;
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