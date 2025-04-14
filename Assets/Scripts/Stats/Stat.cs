public class Stat
{
    public StatTypes StatType { get; set; }
    public float Value { get; set; }

    public enum StatTypes
    {
        MaxHp = 0,
        AttackDamage = 1,
        AbilityPower = 2,
        MaxSpeed = 3,
        VisableDistance = 4,
        ViewingAngle = 5,
        BasicAttackRange = 6,
        SkillRange = 7,
        SkillCooldownTime = 8,
        AttackDamageMultiplier = 9,
        AbilityPowerMultiplier = 10
    }

    public Stat(StatTypes statType, float value)
    {
        StatType = statType;
        Value = value;
    }
}
