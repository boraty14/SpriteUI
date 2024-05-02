using UnityEngine;

namespace BratyUI
{
    public static class ScreenHelper
    {
        // safe area x y is left bottom
        public static Vector2 GetNodePosition(NodeData nodeData, NodeCamera nodeCamera)
        {
            Rect safeArea = Screen.safeArea;
            int width = Screen.width;
            int height = Screen.height;

            var startX = safeArea.x / width;
            var endX = (safeArea.width - safeArea.x) / width;
            var widthRange = new Vector2(startX, endX);
            
            var startY = safeArea.y / height;
            var endY = (safeArea.height - safeArea.y) / height;
            var heightRange = new Vector2(startY, endY);

            var aspect = nodeCamera.Aspect;
            var size = nodeCamera.Size;

            
            
            return Vector2.zero;
        }
        
       //  public static Vector2 GetAnchoredPosition(float anchorX, float anchorY)
       //  {
       //      return GetAnchoredPosition(new Vector2(anchorX, anchorY));
       // }
        
        // public static Vector2 GetAnchoredPosition(Vector2 anchor)
        // {
        //     
        // }
        //
        // public static Vector2 GetPosition(NodeAnchor anchor)
        // {
        //     
        // }
        //
        // public static Vector2 GetSafeAreaPosition

        public static void Log()
        {
            Debug.Log(Screen.safeArea);
            Debug.Log(Screen.currentResolution);
        }
    }
}