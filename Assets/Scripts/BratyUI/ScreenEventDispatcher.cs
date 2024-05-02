using System;
using UnityEngine;

namespace BratyUI
{
    [ExecuteAlways]
    public class ScreenEventDispatcher : MonoBehaviour
    {
        private Rect _safeArea = new Rect();
        private Resolution _currentResolution = new Resolution();
        
        public static event Action OnSafeAreaChange; 
        public static event Action OnResolutionChange; 

        private void Update()
        {
            var resolution = Screen.currentResolution;
            if (resolution.width != _currentResolution.width || resolution.height != _currentResolution.height)
            {
                _currentResolution = resolution;
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