using UnityEditor;
using UnityEngine;

namespace UnityUtility.OldInputSystem
{
    /// <summary>
    /// Returns true if the mouse is over the game view window (or editor game view window).
    /// </summary>
    public class CursorInWindowCheck
    {
        public static bool CursorIsInsideTheGameViewWindow()
        {
            var mousePos = Input.mousePosition;

#if UNITY_EDITOR
            if (mousePos.x == 0 || mousePos.y == 0 ||
                mousePos.x >= Handles.GetMainGameViewSize().x - 1 ||
                mousePos.y >= Handles.GetMainGameViewSize().y - 1)
            {
                return false;
            }
#else
        if (mousePos.x == 0 || mousePos.y == 0 || mousePos.x >= Screen.width - 1 || mousePos.y >= Screen.height - 1) {
            return false;
        }
#endif
            else
            {
                return true;
            }
        }
    }
}