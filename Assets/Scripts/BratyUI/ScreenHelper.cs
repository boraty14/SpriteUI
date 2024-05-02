using UnityEngine;

namespace BratyUI
{
    public static class ScreenHelper
    {
        // safe area x y is left bottom
        // public static Vector3 GetNodePosition(NodeData nodeData, )
        // {
        //     var safeArea = Screen.safeArea;
        //     var width = Screen.width;
        //     var height = Screen.height;
        //     
        //     
        // }
        
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