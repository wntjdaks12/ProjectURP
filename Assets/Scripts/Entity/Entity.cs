using System.Collections;
using UnityEngine;

public class Entity : Data
{
    public Transform Transform { get; private set; }

    public float Lifetime { get; set; }

    public virtual void Init(Transform transform = null)
    {
        Transform = transform;
    }

    public IEnumerator StartLifeTime()
    {
        yield return new WaitForSeconds(Lifetime);

        OnRemoveData();
    }
}
