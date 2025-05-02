using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class HeroController : MonoBehaviour
{
    public HeroObject heroObject;

    private bool istemp;

    private void Start()
    {
        heroObject.SetSkill();

        //StartCoroutine(StartRandomAsync());
    }

    private void Update()
    {
        if (heroObject == null) return;

        var monsters = GameApplication.Instance.GameModel.RunTimeData.ReturnDatas<Monster>(nameof(Monster));

        if (monsters != null)
        {
            if (!istemp)
            {
                istemp = true;
                heroObject.InitSkill();
            }

            var targetMonster = monsters.OrderBy(x => Vector3.Distance(x.Transform.position, heroObject.transform.position)).FirstOrDefault();

            if (targetMonster != null)
            {
                heroObject.OnMove(targetMonster.Transform.position);

                //CheckMp();
            }
        }
    }

    #region 이동 관련 제어
    public IEnumerator StartRandomAsync()
    {
        while (true)
        {
            var point = RandomPoint();
            heroObject.OnMove(point);

            yield return new WaitUntil(() => (transform.position - point).sqrMagnitude < 0.1f);
        }
    }

    private Vector3 RandomPoint()
    {
        NavMeshHit navMeshHit;

        while (true)
        {
            var randCirclePos = Random.insideUnitCircle * 10;
            var spawnPos = transform.position; spawnPos.x += randCirclePos.x; spawnPos.z += randCirclePos.y;
            if (NavMesh.SamplePosition(spawnPos, out navMeshHit, 1f, NavMesh.AllAreas))
            {
                return navMeshHit.position;
            }
        }
    }
    #endregion
    #region 마나 관련 제어

    public void CheckMp()
    {
        if (heroObject.CheckFillMp()) // Mp가 가득 차 있을 경우
        {
            heroObject.OnConsumeMp(); // 마나 소모

            TimeLineManager.Instance.UltSkillEffectTimeLineDirector.Play();

            heroObject.UseSkill(SkillInfo.SkillClassTypes.Ultimate);
        }
        else
        {
            OnRecoverMp(); // 마나 회복
        }
    }

    public void OnRecoverMp()
    {
        heroObject.OnRecoverMp(1);
    }
    #endregion
}