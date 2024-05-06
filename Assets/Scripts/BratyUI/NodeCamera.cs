using System;
using UnityEngine;

namespace BratyUI
{
    [ExecuteAlways]
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
            //Debug.Log("validate camera");
            _camera = GetComponent<Camera>();
            SetCamera();
        }

        private void OnEnable()
        {
            //Debug.Log("enable camera");
            RegisterScreenEvents();
            SetCamera();
        }

        private void OnDisable()
        {
            //Debug.Log("disable camera");
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
            //Debug.LogError($"pixel width {_camera.pixelWidth} scaled pixel width {_camera.scaledPixelWidth}");
        }
        
        private float CalculateCameraSize()
        {
            float horizontalSize = (1f / _camera.aspect) * _cameraSize;
            float cameraSize = Mathf.Lerp(_cameraSize, horizontalSize, _horizontalWeight);
            return cameraSize;
        }
    }
}