using System;
using UnityEngine;

public class Character : Actor, IStat
{
    public enum AttributeTypes 
    {
        Lightning = 0,
        Natura = 1
    }

    public AttributeTypes AttriButeType { get; set; }
    public int SkillId { get; set; }

    public event Action OnDeathEvent;
    public event Action<int> OnHitEvent;
    public event Action<int, int> OnHit2Event;
    public event Action<int> OnHealEvent;
    public event Action<int, int> OnHeal2Event;

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
        StatAbility.CurrentMp = 0;
        StatAbility.CurrentBasicAttackRange = StatAbility.BasicAttackRange;
        // -------------------------------------------------------------------------------------------------------

        OnHitEvent += (damage) =>
        {
            var damagePopupObj = GameApplication.Instance.EntityController.Spawn<DamagePopup, DamagePopupObject>(90001, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, UIManager.Instance.DamagePopupPanel);
            damagePopupObj.UpdteUI(damage);
        };

        OnHit2Event += (damage, count) =>
        {
            var pos = Camera.main.WorldToScreenPoint(transform.position);
            pos.y += count * 30;

            var damagePopupObj = GameApplication.Instance.EntityController.Spawn<DamagePopup, DamagePopupObject>(90001, pos, Quaternion.identity, UIManager.Instance.DamagePopupPanel);
            damagePopupObj.UpdteUI(damage);
        };

        OnHealEvent += (healAmount) =>
        {
            var damagePopupObj = GameApplication.Instance.EntityController.Spawn<DamagePopup, DamagePopupObject>(90002, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, UIManager.Instance.DamagePopupPanel);
            damagePopupObj.UpdteUI(healAmount);
        };

        OnHeal2Event += (healAmount, count) =>
        {
            var pos = Camera.main.WorldToScreenPoint(transform.position);
            pos.y += count * 30;

            var damagePopupObj = GameApplication.Instance.EntityController.Spawn<DamagePopup, DamagePopupObject>(90002, pos, Quaternion.identity, UIManager.Instance.DamagePopupPanel);
            damagePopupObj.UpdteUI(healAmount);
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

    public virtual void OnIdle()
    { 
    }

    public virtual void OnMove()
    {
        StatAbility.CurrentSpeed = StatAbility.MaxSpeed;
    }
        
    public override void OnHit(int damage)
    {
        base.OnHit(damage);

        CalculateHit(damage);

        OnHitEvent?.Invoke(damage);
    }

    public override void OnHit(int damage, int hitCount)
    {
        base.OnHit(damage, hitCount);

        CalculateHit(damage);

        OnHit2Event?.Invoke(damage, hitCount);
    }

    private void CalculateHit(int damage)
    {
        var resCurHp = StatAbility.CurrentHp - damage;

        if (resCurHp > 0)
        {
            StatAbility.CurrentHp = resCurHp;
        }
        else
        {
            StatAbility.CurrentHp = 0;

            OnDeath();
        }
    }

    public override void OnHeal(int healAmount)
    {
        base.OnHeal(healAmount);

        CalculateHeal(healAmount);

        OnHealEvent?.Invoke(healAmount);
    }

    public override void OnHeal(int healAmount, int healCount)
    {
        base.OnHeal(healAmount, healCount);

        CalculateHeal(healAmount);

        OnHeal2Event?.Invoke(healAmount, healCount);
    }

    public virtual void OnDeath()
    {
        OnDeathEvent?.Invoke();
    }

    private void CalculateHeal(int healAmount)
    {
        var resCurHp = StatAbility.CurrentHp + healAmount;

        if (resCurHp >= StatAbility.MaxHp)
        {
            StatAbility.CurrentHp = StatAbility.MaxHp;
        }
        else
        {
            StatAbility.CurrentHp = resCurHp;
        }
    }

    public virtual void OnRecoverMp(int mpAmount)
    {
        var resCurMp = StatAbility.CurrentMp + mpAmount;

        if (resCurMp >= StatAbility.MaxMp)
        {
            StatAbility.CurrentMp = StatAbility.MaxMp;
        }
        else
        {
            StatAbility.CurrentMp = resCurMp;
        }
    }

    public virtual void OnConsumeMp()
    {
        StatAbility.CurrentMp = 0;
    }

    public bool CheckFillMp()
    {
        if (StatAbility.CurrentMp >= StatAbility.MaxMp)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
