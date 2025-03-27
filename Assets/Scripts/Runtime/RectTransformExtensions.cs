using UnityEngine;

public static class RectTransformExtensions
{
    public static void SetWidth(this RectTransform rect, float width)
    {
        rect.sizeDelta = new Vector2(width, rect.rect.height);
    }
}