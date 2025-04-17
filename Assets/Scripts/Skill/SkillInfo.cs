public class SkillInfo : Data
{
    public enum SkillTypes
    {
        Passive,
        Active
    }

    public enum StrategyTypes
    {
        Attack, // 공격
        Heal, // 치유
        Summon, // 소환
    }

    public SkillTypes SkillType { get; set; }

    public StrategyTypes StrategyType { get; set; }

    public int VFXId { get; set; }
    public float CooldwonTime { get; set; }
    public bool Looping { get; set; }
}
