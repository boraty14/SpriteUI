using UnityEngine;

namespace BratyUI
{
    public static class ScreenHelper
    {
        public static void Log()
        {
            Debug.Log(Screen.safeArea);
        }
    }
}
