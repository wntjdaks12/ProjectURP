using System;
using UnityEngine;

public class Character : Actor, IStat
{
    public event Action OnDeathEvent;
    public event Action<int> OnHitEvent;
    public event Action<int> OnHealEvent;

    // IStat 구현
    public StatAbility StatAbility { get; set; }

    public override void Init(Transform transform = null)
    {
        base.Init(transform);

        // 스탯 관련 추가 ----------------------------------------------------------------------------------------
        StatAbility = new StatAbility();

        // 메인 스탯 추가
        var statData = GameApplication.Instance.GameModel.PresetData.ReturnData<StatData>(nameof(StatData), Id);
        StatAbility.AddStatData(StatAbility.StatInfo.StatDataType.Main, statData);

        // 서브 스탯 추가
        var subStatData = new StatData();
        subStatData.Stats = new System.Collections.Generic.List<Stat>();
        subStatData.Stats.Add(new Stat(Stat.StatTypes.MaxHp, 0));
        subStatData.Stats.Add(new Stat(Stat.StatTypes.AttackDamage, 0));
        subStatData.Stats.Add(new Stat(Stat.StatTypes.AbilityPower, 0));
        subStatData.Stats.Add(new Stat(Stat.StatTypes.MaxSpeed, 0));
        StatAbility.AddStatInfo(new StatAbility.StatInfo(StatAbility.StatInfo.StatDataType.Sub, subStatData));

        StatAbility.CurrentSpeed = StatAbility.MaxSpeed;
        StatAbility.CurrentHp = StatAbility.MaxHp;
        StatAbility.CurrentBasicAttackRange = StatAbility.BasicAttackRange;
        // -------------------------------------------------------------------------------------------------------

        OnHitEvent += (damage) =>
        {
            var damagePopupObj = GameApplication.Instance.EntityController.Spawn<DamagePopup, DamagePopupObject>(90001, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, UIManager.Instance.DamageCannvas.transform);
            damagePopupObj.UpdteUI(damage);
        };

        OnHealEvent += (damage) =>
        {
            var damagePopupObj = GameApplication.Instance.EntityController.Spawn<DamagePopup, DamagePopupObject>(90002, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, UIManager.Instance.DamageCannvas.transform);
            damagePopupObj.UpdteUI(damage);
        };

        OnDeathEvent += OnRemoveData;
        OnDeathEvent += () =>   
        {
            var dropItemInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<DropItemInfo>(nameof(DropItemInfo), Id);

            foreach (var data in dropItemInfo.DropItemDatas)
            {
                if (data.DropProbability >= UnityEngine.Random.Range(1, 101))
                {
                    var dropItemObj = GameApplication.Instance.EntityController.Spawn<DropItem, DropItemObject>(70001, Transform.position, Quaternion.identity);
                    dropItemObj.DropItemData = data;
                }

            }
        };
    }

    public override void OnHit(int damage)
    {
        base.OnHit(damage);

        var resCurHp = StatAbility.CurrentHp - damage;
     
        if (resCurHp > 0)
        {
            StatAbility.CurrentHp = resCurHp;
        }
        else
        {
            StatAbility.CurrentHp = 0;

            OnDeathEvent?.Invoke();
        }

        OnHitEvent?.Invoke(damage);
    }

    public override void OnHeal(int healAmount)
    {
        base.OnHeal(healAmount);

        var resCurHp = StatAbility.CurrentHp + healAmount;

        if (resCurHp >= StatAbility.MaxHp)
        {
            StatAbility.CurrentHp = StatAbility.MaxHp;
        }
        else
        {
            StatAbility.CurrentHp = resCurHp;
        }

        OnHealEvent?.Invoke(healAmount);
    }
}
