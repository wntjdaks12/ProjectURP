using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class HeroController : MonoBehaviour, IGameObserver
{
    public HeroObject heroObject;

    private bool istemp;

    private bool isCombat;

    private Coroutine randomAsync;

    private void OnEnable()
    {
        isCombat = false;

        // 관찰자 등록
        GameManager.Instance.Register(this);
    }

    private void Start()
    {
        heroObject.SetSkill(); 
    }

    private void Update()
    {
        if (heroObject == null) return;

        if (!isCombat && randomAsync == null)
        {
            IdleNotify();
        }
        else
        {
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
    }

    private void OnDisable()
    {
        // 관찰자 제거
        GameManager.Instance.Remove(this);
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

        var randCirclePos = Random.insideUnitCircle * 10;
        var spawnPos = transform.position; spawnPos.x += randCirclePos.x; spawnPos.z += randCirclePos.y;
        if (NavMesh.SamplePosition(spawnPos, out navMeshHit, 1f, NavMesh.AllAreas))
        {
            return navMeshHit.position;
        }

        return transform.position;
    }
    #endregion

    #region 옵저버 패턴
    public void IdleNotify()
    {
        if (randomAsync != null)
        {
            StopCoroutine(randomAsync);
        }

        randomAsync = StartCoroutine(StartRandomAsync());

        isCombat = false;
    }

    public void CombatNotify()
    {
        if (randomAsync != null)
        {
            StopCoroutine(randomAsync);
        }

        randomAsync = null;

        isCombat = true;
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