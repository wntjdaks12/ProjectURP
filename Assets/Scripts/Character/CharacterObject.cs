using UnityEngine;
using UnityEngine.AI;

public class CharacterObject : ActorObject
{
    protected NavMeshAgent navMeshAgent;

    private Character character;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public override void Init(Entity entity)
    {
        base.Init(entity);

        character = entity as Character;

        // 네비 메쉬 초기화
        navMeshAgent.speed = character.StatAbility.CurrentSpeed;
    }

    public virtual void OnMove(Vector3 position)
    {
        navMeshAgent.destination = position;
    }

    public override void OnHit(int damage)
    {
        base.OnHit(damage);

        character.OnHit(damage);
    }
}
