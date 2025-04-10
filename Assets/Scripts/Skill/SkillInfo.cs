public class SkillInfo : Data
{
    public enum SkillTypes
    {
        Passive,
        Active
    }

    public enum StrategyTypes
    {
        Attack, // ����
        Heal, // ġ��
        Summon, // ��ȯ
    }

    public SkillTypes SkillType { get; set; }

    public StrategyTypes StrategyType { get; set; }

    public float CastingTime { get; set; } // ���� �ð�
}
