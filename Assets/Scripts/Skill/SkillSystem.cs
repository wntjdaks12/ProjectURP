using System.Collections;
using UnityEngine;

public class SkillSystem
{
    public int Id { get; private set; }

    public SkillInfo SkillInfo { get; private set; } // ��ų ����

    private ISkillBehavior skillBehavior; // ��ų ���� ����

    // IStat ����
    public StatAbility StatAbility { get; set; }

    public CharacterObject Caster { get; private set; }

    public SkillSystem(int id, CharacterObject caster)
    {
        Id = id;
        Caster = caster;

        SkillInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<SkillInfo>(nameof(SkillInfo), id);

        switch (SkillInfo.StrategyType)
        {
            case SkillInfo.StrategyTypes.Attack: SetSkillBehavior(new AttackSkillBehavior(this)); break; // ���� ��ų�� ���
            case SkillInfo.StrategyTypes.Heal: SetSkillBehavior(new HealSkillBehavior(this)); break; // �� ��ų�� ���
            case SkillInfo.StrategyTypes.Summon: SetSkillBehavior(new SummonSkillBehavior(this)); break; // ������ ��ų ��ü ����
        }

        // ���� ���� �߰� ----------------------------------------------------------------------------------------
        StatAbility = new StatAbility();

        // ���� ���� �߰�
        var statData = GameApplication.Instance.GameModel.PresetData.ReturnData<StatData>(nameof(StatData), Id);
        StatAbility.AddStatData(StatAbility.StatInfo.StatDataType.Main, statData);
        // -------------------------------------------------------------------------------------------------------
    }

    public void Init()
    {
        if (SkillInfo.SkillClassType == SkillInfo.SkillClassTypes.Normal) Use();
    }

    // ��ų ���� ���� ����
    public void SetSkillBehavior(ISkillBehavior skillBehavior)
    {
        this.skillBehavior = skillBehavior;
    }

    // ��ų ���
    public void Use()
    {
        CoroutineHelper.StartCoroutine(UseAsync());
    }

    // ��ų ���
    private IEnumerator UseAsync()
    {
        yield return new WaitForSeconds(SkillInfo.CastingTime);

        do
        {
            skillBehavior.Use(Caster);

            yield return new WaitForSeconds(SkillInfo.CooldwonTime);
        } while (SkillInfo.Looping);
    }

    // ������ ���
    public int CalculateDamage()
    {
        // ������ ���ݷ� ��� ��� ���
        var resAttackDamage = (int) (Caster.Character.StatAbility.AttackDamage * StatAbility.AttackDamageMultiplier);
        var resAbilityPower = (int)(Caster.Character.StatAbility.AbilityPower * StatAbility.AbilityPowerMultiplier);

        return resAttackDamage + resAbilityPower;
    }

    public int GetTargetLayer(TargetInfo.TargetType targetType)
    {
        switch (targetType)
        {
            case TargetInfo.TargetType.Self: return 0; // ���� �۾� �ʿ�
            case TargetInfo.TargetType.Ally: return 1 << LayerMask.NameToLayer("Player");
            case TargetInfo.TargetType.Enemy: return 1 << LayerMask.NameToLayer("Enemy");
            case TargetInfo.TargetType.Both: return 0; // ���� �۾� �ʿ�
            default: return 0;
        }
    }

    // ��� ��������
    public CharacterObject[] GetTargets()
    {
        var targets = new CharacterObject[0];

        switch (SkillInfo.TargetType)
        {
            case TargetInfo.TargetType.Self: break; // ���� �۾� �ʿ�
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
            case TargetInfo.TargetType.Enemy: break; // ���� �۾� �ʿ�
            case TargetInfo.TargetType.Both: break; // ���� �۾� �ʿ�
        }

        return targets;
    }
}
