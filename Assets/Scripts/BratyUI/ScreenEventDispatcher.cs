using System;
using UnityEngine;

namespace BratyUI
{
    [ExecuteAlways]
    [DefaultExecutionOrder(-10)]
    public class ScreenEventDispatcher : MonoBehaviour
    {
        private Resolution _resolution;
        public static event Action<Resolution> OnResolutionChange;

        private void OnEnable()
        {
            _resolution = new Resolution();
        }

        private void Update()
        {
            bool isResolutionChanged = false;
            var width = Screen.width;
            var height = Screen.height;
            if (width != _resolution.Width || height != _resolution.Height)
            {
                _resolution.Width = width;
                _resolution.Height = height;
                isResolutionChanged = true;
            }
            
            var safeArea = Screen.safeArea;
            if (!Mathf.Approximately(_resolution.SafeArea.x, safeArea.x) ||
                !Mathf.Approximately(_resolution.SafeArea.y, safeArea.y) ||
                !Mathf.Approximately(_resolution.SafeArea.width, safeArea.width) ||
                !Mathf.Approximately(_resolution.SafeArea.height, safeArea.height))
            {
                _resolution.SafeArea = safeArea;
                isResolutionChanged = true;
            }

            if (isResolutionChanged)
            {
                Debug.LogError(_resolution);
                OnResolutionChange?.Invoke(_resolution);
            }
        }
    }
}