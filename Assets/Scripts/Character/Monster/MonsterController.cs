using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public MonsterObject monsterObject;

    private Transform target;

    public void OnEnable()
    {
        target = null;
    }

    private void Start()
    {
        var character = monsterObject.Entity as Character;
        character.OnHit3Event += (target) =>
        {
            this.target = target;
        };
    }

    private void Update()
    {
        if (monsterObject == null) return;

        if (target != null)
        {
            if (!target.gameObject.activeInHierarchy)
            {
                target = null;
            }
            else
            {
                monsterObject.OnMove(target.position);
            }
        }
        else
        {
            var randPoint = Random.insideUnitSphere * 5;
            randPoint.y = monsterObject.transform.position.y;

            var pos = monsterObject.transform.position;

            NavMeshHit navHit;
            if (NavMesh.SamplePosition(pos + randPoint, out navHit, 1000, NavMesh.AllAreas))
            {
                monsterObject.OnMove(navHit.position);
            }
            else
            {
                monsterObject.OnMove(transform.position);
            }
        }
    }
}