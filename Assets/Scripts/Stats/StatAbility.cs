using System.Collections.Generic;
using System.Linq;

// 스탯 능력치
public class StatAbility
{
    public class StatInfo
    {
        public enum StatDataType { Main, Sub }

        public StatDataType statDataType;
        public StatData statData;

        public StatInfo(StatDataType statType, StatData statData)
        {
            this.statDataType = statType;
            this.statData = statData;
        }
    }

    public List<StatInfo> StatInfos { get; private set; }

    public StatAbility()
    {
        StatInfos = new List<StatInfo>();
    }

    /// <summary>
    /// 스탯 데이터 추가
    /// </summary>
    /// <param name="statDataType">스탯 데이터 타입</param>
    /// <param name="statData">스탯 데이터</param>
    public void AddStatData(StatInfo.StatDataType statDataType, StatData statData)
    {
        StatInfos.Add(new StatInfo(statDataType, statData));
    }

    public void UpdateStatData(StatInfo.StatDataType statDataType, Stat.StatTypes statType, float value)
    {
        var statInfo = StatInfos.Where(x => x.statDataType == statDataType).FirstOrDefault();

        if (statInfo == null)
        {
            UnityEngine.Debug.Log(statDataType);
        }
        else
        {
            statInfo.statData.UpdateStatValue(statType, value);
        }
    }

    /// <summary>
    /// 스텟 데이터 삭제
    /// </summary>
    /// <param name="statDataType">스탯 데이터 타입</param>
    /// <param name="statData">스탯 데이터</param>
    public void RemoveStatData(StatInfo.StatDataType statDataType , StatData statData)
    {
        var statInfo = StatInfos.Where(x => x.statDataType == statDataType && x.statData == statData).FirstOrDefault();

        if (statInfo == null)
        {
        }
        else 
        {
            StatInfos.Remove(statInfo);
        }
    }

    public void AddStatInfo(StatInfo statInfo)
    {
        StatInfos.Add(statInfo);
    }

    public void AddStatInfos(List<StatInfo> statInfos)
    {
        this.StatInfos.AddRange(statInfos);
    }

    // 현재 이동 속도
    private float currentSpeed;
    public float CurrentSpeed
    {
        get { return currentSpeed; }
        set { currentSpeed = value; }
    }
    // 현재 체력
    private int currentHp;
    public int CurrentHp
    {
        get { return currentHp; }
        set { currentHp = value; }
    }
    // 현재 기본 공격 사거리
    private float currentBasicAttackRange;
    public float CurrentBasicAttackRange
    {
        get { return currentBasicAttackRange; }
        set { currentBasicAttackRange = value; }
    }

    // 최대 이동 속도
    public float MaxSpeed
    {
        get { return StatInfos.Sum(x => x.statData.GetTotalStatValue(Stat.StatTypes.MaxSpeed)); }
    }
    // 최대 체력
    public int MaxHp
    {
        get { return (int)StatInfos.Sum(x => x.statData.GetTotalStatValue(Stat.StatTypes.MaxHp)); }
    }
    // 가시 거리
    public float VisableDistance
    {
        get { return StatInfos.Sum(x => x.statData.GetTotalStatValue(Stat.StatTypes.VisableDistance)); }
    }
    // 시야 각
    public float ViewingAngle
    {
        get { return StatInfos.Sum(x => x.statData.GetTotalStatValue(Stat.StatTypes.ViewingAngle)); }
    }
    // 기본 공격 사거리
    public float BasicAttackRange
    {
        get { return StatInfos.Sum(x => x.statData.GetTotalStatValue(Stat.StatTypes.BasicAttackRange)); }
    }
    // 물리 공격력
    public int AttackDamage
    {
        get { return (int)StatInfos.Sum(x => x.statData.GetTotalStatValue(Stat.StatTypes.AttackDamage)); }
    }
    // 마법 공격력
    public int AbilityPower
    {
        get { return (int)StatInfos.Sum(x => x.statData.GetTotalStatValue(Stat.StatTypes.AbilityPower)); }
    }
    // 총 공격력
    public int AttackPower
    {
        get { return AttackDamage + AbilityPower; }
    }
    // 스킬 사거리
    public float SkillRange
    {
        get { return StatInfos.Sum(x => x.statData.GetTotalStatValue(Stat.StatTypes.SkillRange)); }
    }
    // 스킬 쿨타임
    public float SkillCooldownTime
    {
        get { return StatInfos.Sum(x => x.statData.GetTotalStatValue(Stat.StatTypes.SkillCooldownTime)); }
    }
    // 물리 공격력 계수
    public float AttackDamageMultiplier
    {
        get { return StatInfos.Sum(x => x.statData.GetTotalStatValue(Stat.StatTypes.AttackDamageMultiplier)); }
    }
    // 마법 공격력 계수
    public float AbilityPowerMultiplier
    {
        get { return StatInfos.Sum(x => x.statData.GetTotalStatValue(Stat.StatTypes.AbilityPowerMultiplier)); }
    }
}
