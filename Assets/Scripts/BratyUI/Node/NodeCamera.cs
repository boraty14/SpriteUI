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
        
        public Rect SafeArea { get; private set; }
        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }
        
        public float Aspect => _camera.aspect;
        public float Size => _camera.orthographicSize;
        public bool IsInitialized => ScreenWidth != 0;

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
            SafeArea = Screen.safeArea;
            ScreenWidth = Screen.width;
            ScreenHeight = Screen.height;
            
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