using System;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public event Action OnHitEndEvent;


    public void OnHitEnd()
    {
        OnHitEndEvent?.Invoke();
    }

}
