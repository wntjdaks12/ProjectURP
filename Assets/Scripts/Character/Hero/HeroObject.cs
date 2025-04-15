using UnityEngine;

public class HeroObject : CharacterObject
{
    public Hero Hero { get; private set; }

    public SkillSystem skillSystem;

    public override void Init(Entity entity)
    {
        base.Init(entity);

        Hero = entity as Hero;

        ChangeLayersRecursively(transform, "Player");

        skillSystem = new SkillSystem(50001, this);// 임시
    }

    public virtual void SetExp(int exp)
    {
        Hero.SetExp(exp);
    }


    // 현재 이동 속도
    private float currentSpeed;
    public float CurrentSpeed
    {
        get { return currentSpeed; }
        set { currentSpeed = value; }
    }
}
