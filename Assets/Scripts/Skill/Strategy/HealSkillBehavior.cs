using System.Collections;
using UnityEngine;

public class HealSkillBehavior : ISkillBehavior
{
    public SkillSystem SkillSystem { get; set; }

    public HealSkillBehavior(SkillSystem skillSystem)
    {
        SkillSystem = skillSystem;
    }

    public void Use(CharacterObject caster)
    {
        if (caster == null) return;

        GameApplication.Instance.EntityController.Spawn<VFX, VFXObject>(SkillSystem.SkillInfo.VFXId, Vector3.zero, Quaternion.identity, caster.transform);
    }
}
