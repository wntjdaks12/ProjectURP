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

        if (SkillInfo.SkillType == SkillInfo.SkillTypes.Passive)
        {
            if (!SkillInfo.Looping)
            {
                Use(caster);
            }
            else
            {
                CoroutineHelper.StartCoroutine(UseAsync(caster));
            }
        }
    }

    // 스킬 전략 행위 세팅
    public void SetSkillBehavior(ISkillBehavior skillBehavior)
    {
        this.skillBehavior = skillBehavior;
    }

    // 스킬 사용
    public void Use(CharacterObject caster)
    {
        skillBehavior.Use(caster);
    }

    public IEnumerator UseAsync(CharacterObject caster)
    {
        while (true)
        {
            skillBehavior.Use(caster);

            yield return new WaitForSeconds(SkillInfo.CooldwonTime);
        }
    }

    // 데미지 계산
    public int CalculateDamage()
    {
        // 시전자 공격력 비례 계수 계산
        var resAttackDamage = (int) (Caster.Character.StatAbility.AttackDamage * StatAbility.AttackDamageMultiplier);
        var resAbilityPower = (int)(Caster.Character.StatAbility.AbilityPower * StatAbility.AbilityPowerMultiplier);

        return resAttackDamage + resAbilityPower;
    }
}
