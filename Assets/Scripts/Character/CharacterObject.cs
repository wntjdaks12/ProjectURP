using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class CharacterObject : ActorObject
{
    protected NavMeshAgent navMeshAgent;

    public Character Character { get; private set; }

    private List<SkillSystem> SkillSystems;

    private ICharacterState state;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        SkillSystems = new List<SkillSystem>();

        if (animationHandler != null)
        {
            animationHandler.OnHitEndEvent += () =>
            {
                // 아직 움직이는 코드 안짜서 아이들로만 고정
                OnIdle();
            };
        }
    }

    public override void Init(Entity entity)
    {
        base.Init(entity);

        Character = entity as Character;

        // 네비 메쉬 초기화
        navMeshAgent.speed = Character.StatAbility.CurrentSpeed;

        // 상태 idle
        SetState(CharacterIdleState.Instance);
        OnIdle();
    }

    public void SetState(ICharacterState state)
    {
        this.state = state;
    }

    public virtual void OnIdle()
    {
        Character.OnIdle();

        state.OnIdle(this);
    }


    public virtual void OnMove(Vector3 position)
    {
        Character.OnMove();

        state.OnMove(this);

        navMeshAgent.destination = position;
        navMeshAgent.speed = Character.StatAbility.CurrentSpeed;
    }

    public override void OnHit(int damage)
    {
        base.OnHit(damage);

        Character.OnHit(damage);

        state.OnHit(this);

        ChangeMaterial( 0.05f);
    }

    public override void OnHit(int damage, int hitCount)
    {
        base.OnHit(damage, hitCount);

        Character.OnHit(damage,  hitCount);

        ChangeMaterial(0.05f);
    }

    public override void OnHit(int damage, int hitCount, Transform target)
    {
        base.OnHit(damage, hitCount, target);

        Character.OnHit(damage, hitCount, target);

        ChangeMaterial(0.05f);
    }

    public override void OnHeal(int healAmount)
    {
        base.OnHeal(healAmount);

        Character.OnHeal(healAmount);
    }


    public override void OnHeal(int healAmount, int healCount)
    {
        base.OnHeal(healAmount);

        Character.OnHeal(healAmount, healCount);
    }

    public virtual void OnDeath()
    {
        Character.OnDeath();

        state.OnDeath(this);
    }

    public virtual void OnRecoverMp(int mpAmount)
    {
        Character.OnRecoverMp(mpAmount);
    }

    public virtual void OnConsumeMp()
    {
        Character.OnConsumeMp();
    }

    public bool CheckFillMp()
    {
        return Character.CheckFillMp();
    }

    #region 스킬 관련
    public void SetSkill()
    {
        SkillSystems.Add(new SkillSystem(Character.SkillId, this));
    }

    public SkillSystem GetSkill(int skillId)
    {
        return SkillSystems.Where(x => x.Id == skillId).FirstOrDefault();
    }

    public void InitSkill()
    {
        for (int i = 0; i < SkillSystems.Count; i++)
        {
            SkillSystems[i].Init();
        }
    }

    public void UseSkill(SkillInfo.SkillClassTypes skillClassType)
    {
        var skillSystem = SkillSystems.Where(x => x.SkillInfo.SkillClassType == skillClassType).FirstOrDefault();

        if (skillSystem != null) skillSystem.Use();
    }
    #endregion
}
