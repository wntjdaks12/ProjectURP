using UnityEngine;
using UnityEngine.UI;

public static class CanvasScalerExtension
{
    public static Vector2 GetScreenUIOffset(this CanvasScaler canvasScaler)
    { 
        var screenUIOffset = Vector2.zero;

        var y = canvasScaler.referenceResolution.y * Screen.width / canvasScaler.referenceResolution.x;
        screenUIOffset.x = Screen.width / canvasScaler.referenceResolution.x;
        screenUIOffset.y = y / canvasScaler.referenceResolution.y;

        return screenUIOffset;
    }
}
