public class AttackSkillBehavior : ISkillBehavior
{
    public SkillSystem SkillSystem { get; set; }

    public AttackSkillBehavior(SkillSystem skillSystem)
    {
        SkillSystem = skillSystem;
    }

    public void Use(CharacterObject caster)
    {
    }
}
