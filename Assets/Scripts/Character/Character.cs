using System;
using UnityEngine;

public class Character : Actor, IStat
{
    public event Action OnDeathEvent;

    // IStat ����
    public StatAbility StatAbility { get; set; }

    public override void Init(Transform transform = null)
    {
        base.Init(transform);

        // ���� ���� �߰� ----------------------------------------------------------------------------------------
        StatAbility = new StatAbility();

        // ���� ���� �߰�
        var statData = GameApplication.Instance.GameModel.PresetData.ReturnData<StatData>(nameof(StatData), Id);
        StatAbility.AddStatData(StatAbility.StatInfo.StatDataType.Main, statData);

        StatAbility.CurrentSpeed = StatAbility.MaxSpeed;
        StatAbility.CurrentHp = StatAbility.MaxHp;
        StatAbility.CurrentBasicAttackRange = StatAbility.BasicAttackRange;
        // -------------------------------------------------------------------------------------------------------

        OnDeathEvent += OnRemoveData;
    }

    public override void OnHit(int damage)
    {
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
    }
}
