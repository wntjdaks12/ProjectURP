using UnityEngine;

public class ActorObject : EntityObject
{
    [field: SerializeField] public Transform HitNode { get; private set; }

    public override void Init(Entity entity)
    {
        base.Init(entity);
    }

    public virtual void OnHit(int damage)
    { 
    }
}
