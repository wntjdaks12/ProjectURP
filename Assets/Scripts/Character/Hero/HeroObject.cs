using UnityEngine;

public class HeroObject : CharacterObject
{
    public Hero Hero { get; private set; }

    public override void Init(Entity entity)
    {
        base.Init(entity);

        Hero = entity as Hero;

        ChangeLayersRecursively(transform, "Player");

        var skill = new SkillSystem(50001, this);// �ӽ�
    }

    public virtual void SetExp(int exp)
    {
        Hero.SetExp(exp);
    }


    // ���� �̵� �ӵ�
    private float currentSpeed;
    public float CurrentSpeed
    {
        get { return currentSpeed; }
        set { currentSpeed = value; }
    }
}
