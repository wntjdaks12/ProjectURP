using System.Collections.Generic;
using UnityEngine;

public class ColliderSensor : MonoBehaviour
{
    public int damage = 10;
    public float damageInterval = 1f;

    private Dictionary<CharacterObject, float> lastHitTime;

    private void Awake()
    {
        lastHitTime = new Dictionary<CharacterObject, float>();
    }

    private void OnTriggerStay(Collider other)
    {
        var target = other.GetComponentInParent<CharacterObject>();

        if (target != null)
        {
            if (!lastHitTime.ContainsKey(target))
            {
                target.OnHit(damage);
                lastHitTime[target] = Time.time;
            }

            if (Time.time - lastHitTime[target] >= damageInterval)
            {
                target.OnHit(damage);
                lastHitTime[target] = Time.time;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var target = other.GetComponentInParent<CharacterObject>();

        if (target != null && lastHitTime.ContainsKey(target))
        {
            lastHitTime.Remove(target);
        }
    }
}
