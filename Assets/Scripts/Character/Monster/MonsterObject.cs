using UnityEngine;

public class MonsterObject : CharacterObject
{
    public override void Init(Entity entity)
    {
        base.Init(entity);

        ChangeLayersRecursively(transform, "Enemy");
    }
}
