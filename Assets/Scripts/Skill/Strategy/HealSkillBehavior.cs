using UnityEngine;

// ½ºÅ³ Àü·« - Èú
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

       var targets = SkillSystem.GetTargets();

        for (int i = 0; i < targets.Length; i++)
        {
            GameApplication.Instance.EntityController.Spawn<VFX, VFXObject>(SkillSystem.SkillInfo.VFXId, Vector3.zero, Quaternion.identity, targets[i].transform);

            targets[i].OnHeal(SkillSystem.CalculateDamage());
        }
    }
}
