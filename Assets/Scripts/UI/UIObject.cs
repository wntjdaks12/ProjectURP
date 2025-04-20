using UnityEngine;

public class UIObject : EntityObject
{
    protected RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
}
