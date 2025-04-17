using UnityEngine;
using UnityEngine.AI;

public class CharacterObject : ActorObject
{
    protected NavMeshAgent navMeshAgent;

    public Character Character { get; private set; }

    public SkillSystem SkillSystem { get; private set; }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
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
        navMeshAgent.destination = position;
    }

    public override void OnHit(int damage)
    {
        base.OnHit(damage);

        Character.OnHit(damage);
    }

    public void SetSkill(int skillId)
    {
        SkillSystem = new SkillSystem(skillId, this);
    }
}
