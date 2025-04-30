using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class View : MonoBehaviour
{
    public UnityEvent OnShowEvent;
    public UnityEvent OnHideEvent;

    public virtual void OnShow()
    {
        gameObject.SetActive(true);

        OnShowEvent?.Invoke();
    }

    public virtual void OnHide()
    {
        gameObject.SetActive(false);

        OnHideEvent?.Invoke();
    }

    public abstract void UpdateUI();
}
