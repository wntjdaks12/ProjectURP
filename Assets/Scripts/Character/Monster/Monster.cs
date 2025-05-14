using UnityEngine;

public class Monster : Character
{
    public int Exp { get; set; }

    public override void Init(Transform transform = null)
    {
        base.Init(transform);

        OnDeathEvent += () =>
        {
            var dropItemInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<DropItemInfo>(nameof(DropItemInfo), Id);

            foreach (var data in dropItemInfo.DropItemDatas)
            {
                if (data.DropProbability >= UnityEngine.Random.Range(1, 101))
                {
                    GameApplication.Instance.EntityController.Spawn<VFX, VFXObject>(40006, Transform.position, Quaternion.identity);
                    GameApplication.Instance.EntityController.Spawn<DropItem, DropItemObject>(70001, Transform.position, Quaternion.identity);
                }

            }
        };
    }
}
