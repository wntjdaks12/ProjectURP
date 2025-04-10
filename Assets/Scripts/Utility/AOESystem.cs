using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AOESystem : MonoBehaviour
{
    private List<ActorObject> cacheColliders;

    private float curTime;

    [SerializeField] private SkillObject skillObject;

    private void Awake()
    {
        cacheColliders = new List<ActorObject>();
    }

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        curTime = 0;
    }

    private void Update()
    {
        if (curTime == 0)
        {
            var colliders = Physics.OverlapSphere(transform.position, 7, skillObject.GetTargetLayer()).Select(x => x.GetComponentInParent<ActorObject>()).ToList();

            if (cacheColliders.Count <= 0)
            {
                cacheColliders = colliders;
            }
            else
            {
                var resList1 = colliders.Except(cacheColliders).ToList();
                var resList2 = cacheColliders.Intersect(colliders).ToList();

                resList1.AddRange(resList2);

                cacheColliders = resList1;
            }

            foreach (var actorObj in cacheColliders)
            {
                skillObject.CreateVFX(actorObj.HitNode.position, Quaternion.identity);

                actorObj.OnHit(skillObject.SkillSystem.StatAbility.AttackPower);
            }
        }

        curTime += Time.deltaTime;

        if (curTime >= 0.1f)
        {
            curTime = 0;
        }
    }
}
