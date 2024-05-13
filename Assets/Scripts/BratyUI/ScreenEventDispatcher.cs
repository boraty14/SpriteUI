using System;
using UnityEngine;

namespace BratyUI
{
    [ExecuteAlways]
    public class ScreenEventDispatcher : MonoBehaviour
    {
        private Rect _safeArea;
        private int _height;
        private int _width;
        
        public static event Action OnSafeAreaChange; 
        public static event Action OnResolutionChange;

        private void OnEnable()
        {
            _width = 0;
            _height = 0;
            _safeArea = new Rect();
        }

        private void Update()
        {
            var width = Screen.width;
            var height = Screen.height;
            if (width != _width || height != _height)
            {
                _width = width;
                _height = height;
                OnResolutionChange?.Invoke();
            }
            
            var safeArea = Screen.safeArea;
            if (!Mathf.Approximately(_safeArea.x, safeArea.x) ||
                !Mathf.Approximately(_safeArea.y, safeArea.y) ||
                !Mathf.Approximately(_safeArea.width, safeArea.width) ||
                !Mathf.Approximately(_safeArea.height, safeArea.height))
            {
                _safeArea = safeArea;
                OnSafeAreaChange?.Invoke();
            }
        }
    }
}