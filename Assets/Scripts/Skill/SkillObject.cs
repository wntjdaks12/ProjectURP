using UnityEngine;
using System.Linq;

public class SkillObject : EntityObject
{
    private Skill skill;

    public SkillSystem SkillSystem { get; set; }

    public override void Init(Entity entity)
    {
        base.Init(entity);

        skill = entity as Skill;

        GameApplication.Instance.EntityController.Spawn<VFX, VFXObject>(skill.VFXId, transform.position, Quaternion.identity, transform);
    }

    public int GetTargetLayer()
    {
        switch (skill.TargetType)
        {
            case TargetInfo.TargetType.Self: return 0; // ���� �۾� �ʿ�
            case TargetInfo.TargetType.Ally: return 1 << LayerMask.NameToLayer("Player");
            case TargetInfo.TargetType.Enemy: return 1 << LayerMask.NameToLayer("Enemy");
            case TargetInfo.TargetType.Both: return 0; // ���� �۾� �ʿ�
        }

        return 0;
    }

    public void CreateVFX(Vector3 position, Quaternion rotation)
    {
        GameApplication.Instance.EntityController.Spawn<VFX, VFXObject>(skill.HitVFXId, position, rotation);
    }
}
