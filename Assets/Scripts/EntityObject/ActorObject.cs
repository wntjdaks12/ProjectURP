using UnityEngine;

public class ActorObject : EntityObject
{
    [field: SerializeField] public Transform HitNode { get; private set; }
    [field: SerializeField] public Transform MiniHUDNode { get; private set; }

    public override void Init(Entity entity)
    {
        base.Init(entity);
    }

    public virtual void OnHit(int damage)
    { 
    }

    public virtual void OnHit(int damage, int hitCount)
    {
    }

    public virtual void OnHeal(int healAmount)
    {
    }

    public virtual void OnHeal(int healAmount, int healCount)
    {
    }
}
