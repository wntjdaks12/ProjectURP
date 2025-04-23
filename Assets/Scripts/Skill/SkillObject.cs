using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class SkillObject : EntityObject
{
    private Skill skill;

    public SkillSystem SkillSystem { get; set; }

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
        return Physics.OverlapSphere(transform.position, 7, SkillSystem.GetTargetLayer(skill.TargetType)).Select(x => x.GetComponentInParent<ActorObject>()).ToList();
    }
}