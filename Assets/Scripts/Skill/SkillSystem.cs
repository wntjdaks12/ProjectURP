using System.Collections;
using UnityEngine;

public class SkillSystem
{
    public int Id { get; private set; }

    public SkillInfo SkillInfo { get; private set; } // 스킬 정보

    private ISkillBehavior skillBehavior; // 스킬 전략 행위

    // IStat 구현
    public StatAbility StatAbility { get; set; }

    public CharacterObject Caster { get; private set; }

    public SkillSystem(int id, CharacterObject caster)
    {
        Id = id;
        Caster = caster;

        SkillInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<SkillInfo>(nameof(SkillInfo), id);

        switch (SkillInfo.StrategyType)
        {
            case SkillInfo.StrategyTypes.Attack: SetSkillBehavior(new AttackSkillBehavior(this)); break; // 공격 스킬일 경우
            case SkillInfo.StrategyTypes.Heal: SetSkillBehavior(new HealSkillBehavior(this)); break; // 힐 스킬일 경우
            case SkillInfo.StrategyTypes.Summon: SetSkillBehavior(new SummonSkillBehavior(this)); break; // 독립된 스킬 개체 생성
        }

        // 스탯 관련 추가 ----------------------------------------------------------------------------------------
        StatAbility = new StatAbility();

        // 메인 스탯 추가
        var statData = GameApplication.Instance.GameModel.PresetData.ReturnData<StatData>(nameof(StatData), Id);
        StatAbility.AddStatData(StatAbility.StatInfo.StatDataType.Main, statData);
        // -------------------------------------------------------------------------------------------------------
    }

    public void Init()
    {
        if (SkillInfo.SkillClassType == SkillInfo.SkillClassTypes.Normal) Use();
    }

    // 스킬 전략 행위 세팅
    public void SetSkillBehavior(ISkillBehavior skillBehavior)
    {
        this.skillBehavior = skillBehavior;
    }

    // 스킬 사용
    public void Use()
    {
        CoroutineHelper.StartCoroutine(UseAsync());
    }

    // 스킬 사용
    private IEnumerator UseAsync()
    {
        yield return new WaitForSeconds(SkillInfo.CastingTime);

        do
        {
            skillBehavior.Use(Caster);

            yield return new WaitForSeconds(SkillInfo.CooldwonTime);
        } while (SkillInfo.Looping);
    }

    // 데미지 계산
    public int CalculateDamage()
    {
        // 시전자 공격력 비례 계수 계산
        var resAttackDamage = (int) (Caster.Character.StatAbility.AttackDamage * StatAbility.AttackDamageMultiplier);
        var resAbilityPower = (int)(Caster.Character.StatAbility.AbilityPower * StatAbility.AbilityPowerMultiplier);

        return resAttackDamage + resAbilityPower;
    }

    public int GetTargetLayer(TargetInfo.TargetType targetType)
    {
        switch (targetType)
        {
            case TargetInfo.TargetType.Self: return 0; // 수정 작업 필요
            case TargetInfo.TargetType.Ally: return 1 << LayerMask.NameToLayer("Player");
            case TargetInfo.TargetType.Enemy: return 1 << LayerMask.NameToLayer("Enemy");
            case TargetInfo.TargetType.Both: return 0; // 수정 작업 필요
            default: return 0;
        }
    }

    // 대상 가져오기
    public CharacterObject[] GetTargets()
    {
        var targets = new CharacterObject[0];

        switch (SkillInfo.TargetType)
        {
            case TargetInfo.TargetType.Self: break; // 수정 작업 필요
            case TargetInfo.TargetType.Ally:
                if (Caster.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    targets = GameApplication.Instance.GameModel.RunTimeData.ReturnDatas<HeroObject>(nameof(HeroObject));
                }
                else if (Caster.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    targets = GameApplication.Instance.GameModel.RunTimeData.ReturnDatas<MonsterObject>(nameof(MonsterObject));
                }
                break;
            case TargetInfo.TargetType.Enemy: break; // 수정 작업 필요
            case TargetInfo.TargetType.Both: break; // 수정 작업 필요
        }

        return targets;
    }
}
