using System.Collections.Generic;
using UnityEngine;

public class CharacterBodyCollider : MonoBehaviour
{
    [SerializeField] private CharacterObject characterObject;

    private float damageInterval = 1f;

    private Dictionary<CharacterObject, float> hits;

    private void Awake()
    {
        hits = new Dictionary<CharacterObject, float>();
    }

    private void OnTriggerStay(Collider other)
    {
        var target = other.GetComponentInParent<CharacterObject>();

        if (target != null)
        {
            if (!hits.ContainsKey(target))
            {
                target.OnHit(characterObject.Character.StatAbility.BodyDamage);
                hits[target] = Time.time;
            }

            if (Time.time - hits[target] >= damageInterval)
            {
                target.OnHit(characterObject.Character.StatAbility.BodyDamage);
                hits[target] = Time.time;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var target = other.GetComponentInParent<CharacterObject>();

        if (target != null && hits.ContainsKey(target))
        {
            hits.Remove(target);
        }
    }
}
