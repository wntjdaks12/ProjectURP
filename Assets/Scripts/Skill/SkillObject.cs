using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class SkillObject : EntityObject, IGameObserver
{
    private Skill skill;

    public SkillSystem SkillSystem { get; set; }

    private void OnEnable()
    {
        // 관찰자 등록
        GameManager.Instance.Register(this);
    }

    public override void Init(Entity entity)
    {
        base.Init(entity);

        skill = entity as Skill;

        GameApplication.Instance.EntityController.Spawn<VFX, VFXObject>(skill.VFXId, Vector3.zero, Quaternion.identity, transform);
    }

    public void CreateVFX(Vector3 position, Quaternion rotation)
    {
        GameApplication.Instance.EntityController.Spawn<VFX, VFXObject>(skill.HitVFXId, position, rotation);
    }

    public List<ActorObject> GetTargets()
    {
        return Physics.OverlapSphere(transform.position, 7, SkillSystem.GetTargetLayer(SkillSystem.SkillInfo.TargetType)).Select(x => x.GetComponentInParent<ActorObject>()).ToList();
    }

    private void OnDisable()
    {
        // 관찰자 제거
        GameManager.Instance.Remove(this);
    }

    public void StartCombatNotify()
    {
    }

    public void CombatNotify()
    {
    }

    public void IdleNotify()
    {
    }

    public void EndCombatNotify()
    {
        OnRemoveEntity();
    }
}