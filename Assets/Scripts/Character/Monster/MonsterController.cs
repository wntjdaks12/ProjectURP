using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public MonsterObject monsterObject;

    private void Update()
    {
        if (monsterObject == null) return;

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
            monsterObject.OnMove (transform.position);
        }
    }
}