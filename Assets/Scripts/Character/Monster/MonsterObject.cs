using UnityEngine;

public class MonsterObject : CharacterObject
{
    public Monster Monster { get; private set; }


    public MiniHUDObject minihudObject { get; private set; }


    public override void Init(Entity entity)
    {
        base.Init(entity);

        Monster = Entity as Monster;

        gameObject.ChangeLayersRecursively("Enemy");
        gameObject.ChangeTagsRecursively("Monster");

        minihudObject = GameApplication.Instance.EntityController.Spawn<MiniHUD, MiniHUDObject>(110001, Camera.main.WorldToScreenPoint(MiniHUDNode.position), Quaternion.identity, UIManager.Instance.MiniHUDPanel);
        minihudObject.Init(this, Monster.StatAbility);
    }
}
