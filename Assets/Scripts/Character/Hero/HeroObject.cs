using UnityEngine;

public class HeroObject : CharacterObject
{
    public override void Init(Entity entity)
    {
        base.Init(entity);

        ChangeLayersRecursively(transform, "Player");
    }
}
