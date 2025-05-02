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

        // ������ ���
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
        // ������ ����
        GameManager.Instance.Remove(this);
    }

    #region �̵� ���� ����
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

    #region ������ ����
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

    #region ���� ���� ����
    public void CheckMp()
    {
        if (heroObject.CheckFillMp()) // Mp�� ���� �� ���� ���
        {
            heroObject.OnConsumeMp(); // ���� �Ҹ�

            TimeLineManager.Instance.UltSkillEffectTimeLineDirector.Play();

            heroObject.UseSkill(SkillInfo.SkillClassTypes.Ultimate);
        }
        else
        {
            OnRecoverMp(); // ���� ȸ��
        }
    }

    public void OnRecoverMp()
    {
        heroObject.OnRecoverMp(1);
    }
    #endregion
}