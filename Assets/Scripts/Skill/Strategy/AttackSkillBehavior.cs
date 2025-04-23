
// 스킬 전략 - 공격
using System.Collections;
using System.Linq;
using UnityEngine;

public class AttackSkillBehavior : ISkillBehavior
{
    public SkillSystem SkillSystem { get; set; }

    public AttackSkillBehavior(SkillSystem skillSystem)
    {
        SkillSystem = skillSystem;
    }

    public void Use(CharacterObject caster)
    {
        var colliders = Physics.OverlapSphere(SkillSystem.Caster.transform.position, 7, SkillSystem.GetTargetLayer(TargetInfo.TargetType.Enemy)).Select(x => x.GetComponentInParent<ActorObject>()).ToList();

        foreach (var actorObj in colliders)
        {
            for (int i = 0; i < SkillSystem.SkillInfo.HitDatas.Count; i++)
            {
                CoroutineHelper.StartCoroutine(HitAysnc(i, actorObj, SkillSystem.SkillInfo.HitDatas[i]));
            }
        }
    }

    private IEnumerator HitAysnc(int index, ActorObject actorObject, HitData hitData)
    {
        yield return new WaitForSeconds(hitData.Delay);

        actorObject.OnHit(10, index);
    }
}
