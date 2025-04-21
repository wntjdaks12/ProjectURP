using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class CharacterObject : ActorObject
{
    protected NavMeshAgent navMeshAgent;

    public Character Character { get; private set; }

    private List<SkillSystem> SkillSystems;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        SkillSystems = new List<SkillSystem>();
    }

    public override void Init(Entity entity)
    {
        base.Init(entity);

        Character = entity as Character;

        // 네비 메쉬 초기화
        navMeshAgent.speed = Character.StatAbility.CurrentSpeed;
    }


    public virtual void OnMove(Vector3 position)
    {
        Character.OnMove();

        navMeshAgent.destination = position;
        navMeshAgent.speed = Character.StatAbility.CurrentSpeed;
    }

    public override void OnHit(int damage)
    {
        base.OnHit(damage);

        Character.OnHit(damage);
    }

    public override void OnHeal(int healAmount)
    {
        base.OnHeal(healAmount);

        Character.OnHeal(healAmount);
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

    public void SetSkill(int skillId)
    {
        SkillSystems.Add(new SkillSystem(skillId, this));
    }

    public SkillSystem GetSkill(int skillId)
    {
        return SkillSystems.Where(x => x.Id == skillId).FirstOrDefault();
    }
}
