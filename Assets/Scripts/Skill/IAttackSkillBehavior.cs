public class IAttackSkillBehavior : ISkillBehavior
{
    public SkillSystem SkillSystem { get; set; }

    public IAttackSkillBehavior(SkillSystem skillSystem)
    {
        SkillSystem = skillSystem;
    }

    public void Use(CharacterObject caster)
    {
    }
}
