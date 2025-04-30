using System;
using UnityEngine;
using UnityEngine.Events;

public class ClickSystem : MonoBehaviour
{
    public UnityEvent<GameObject> OnCliked;

    public void OnClick(GameObject gameObject)
    {
        OnCliked?.Invoke(gameObject);
    }
}
