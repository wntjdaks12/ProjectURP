using System;
using UnityEngine;

public class Character : Actor, IStat
{
    public event Action OnDeathEvent;

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

        StatAbility.CurrentSpeed = StatAbility.MaxSpeed;
        StatAbility.CurrentHp = StatAbility.MaxHp;
        StatAbility.CurrentBasicAttackRange = StatAbility.BasicAttackRange;
        // -------------------------------------------------------------------------------------------------------

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
