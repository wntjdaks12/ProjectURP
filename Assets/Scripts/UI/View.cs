using System;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    public event Action OnShowEvent;
    public event Action OnHideEvent;

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
