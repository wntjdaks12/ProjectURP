public class Actor : Entity
{
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
