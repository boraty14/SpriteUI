using UnityEngine;

namespace BratyUI
{
    public static class ScreenHelper
    {
        // safe area x y is left bottom
        public static Vector3 GetNodePosition(NodeData nodeData, NodeData parentNodeData)
        {
            //without pivot
            
            
            
            // var parentPosition = parentNodeData.Position;
            // var parentSize = parentNodeData.Size;
            // var leftBottom = new Vector2(parentPosition.x - parentSize.x * 0.5f,
            //     parentPosition.y - parentSize.y * 0.5f);
            // var rightTop = new Vector2(parentPosition.x + parentSize.x * 0.5f,
            //     parentPosition.y + parentSize.y * 0.5f);
            //
            // var 
            // float xPosition = 
            
            

            
            
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