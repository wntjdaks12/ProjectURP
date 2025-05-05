using UnityEngine;

public class MonsterObject : CharacterObject
{
    public Monster Monster { get; private set; }

    public override void Init(Entity entity)
    {
        base.Init(entity);

        Monster = Entity as Monster;

        gameObject.ChangeLayersRecursively("Enemy");
        gameObject.ChangeTagsRecursively("Monster");
    }
}
