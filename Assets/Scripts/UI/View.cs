using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class View : MonoBehaviour
{
    public UnityEvent OnShowEvent;
    public UnityEvent OnHideEvent;

    public virtual void OnShow()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);

            OnShowEvent?.Invoke();
        }
    }

    public virtual void OnHide()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);

            OnHideEvent?.Invoke();
        }
    }

    public abstract void UpdateUI();
}
