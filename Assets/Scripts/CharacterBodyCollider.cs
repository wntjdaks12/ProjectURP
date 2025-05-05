using System.Collections.Generic;
using UnityEngine;

public class CharacterBodyCollider : MonoBehaviour
{
    [SerializeField] private TargetInfo.TargetType targetType;

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

        if (GetTarget(other))
        {
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
    }

    private void OnTriggerExit(Collider other)
    {
        var target = other.GetComponentInParent<CharacterObject>();

        if (target != null && hits.ContainsKey(target))
        {
            hits.Remove(target);
        }
    }

    // 임시로 짠 타겟 로직
    private bool GetTarget(Collider other)
    {
        switch (targetType)
        {
            case TargetInfo.TargetType.Self: return false; // 수정 작업 필요
            case TargetInfo.TargetType.Ally:
                if (characterObject.tag == "Hero")
                {
                    if (other.tag == "Hero")
                    {
                        return true;
                    }
                    else if (other.tag == "Monster")
                    {
                        return false;
                    }
                }
                else if (characterObject.tag == "Monster")
                {
                    if (other.tag == "Hero")
                    {
                        return false;
                    }
                    else if (other.tag == "Monster")
                    {
                        return true;
                    }
                }
                return true;
            case TargetInfo.TargetType.Enemy:
                if (characterObject.tag == "Hero")
                {
                    if (other.tag == "Hero")
                    {
                        return false;
                    }
                    else if (other.tag == "Monster")
                    {
                        return true;
                    }
                }
                else if (characterObject.tag == "Monster")
                {
                    if (other.tag == "Hero")
                    {
                        return true;
                    }
                    else if (other.tag == "Monster")
                    {
                        return false;
                    }
                }
                return true;
            case TargetInfo.TargetType.Both: return false; // 수정 작업 필요
        }

        return false;
    }
}
