using System.Collections.Generic;

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

    public enum SkillClassTypes
    { 
        Normal,
        Ultimate
    }

    public SkillTypes SkillType { get; set; }

    public StrategyTypes StrategyType { get; set; }

    public SkillClassTypes SkillClassType { get; set; }

    public TargetInfo.TargetType TargetType { get; set; }

    public int VFXId { get; set; }

    public float CastingTime { get; set; }
    public float CooldwonTime { get; set; }
    public bool Looping { get; set; }

    public List<HitData> HitDatas { get; set; }
    public List<HitData> HealDatas { get; set; }
}

public class HitData 
{
    public float Delay { get; set; }
}

public class HealData
{
    public float Delay { get; set; }
}