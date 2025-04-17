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
    }
}
