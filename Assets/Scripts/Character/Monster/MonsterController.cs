using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public MonsterObject monsterObject;

    private Transform target;

    private bool hasArrived; // 도착 지점에 있는지

    private IEnumerator checkStateAsync;

    private MonsterAIData monsterAIData; // 몬스터 ai 데이터

    private MiniHUDObject minihudObject;

    private void Awake()
    {
        Debug.Assert(monsterObject != null, "몬스터 오브젝트 없음");

        monsterAIData = Resources.Load<MonsterAIData>("ScriptableObject/MonsterAI/FiledMonsterAIData");
        Debug.Assert(monsterAIData != null, "몬스터 AI 데이터 Null");
    }

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

        minihudObject = GameApplication.Instance.EntityController.Spawn<MiniHUD, MiniHUDObject>(110001, Camera.main.WorldToScreenPoint(monsterObject.MiniHUDNode.position), Quaternion.identity, UIManager.Instance.MiniHUDPanel);
        minihudObject.Init(monsterObject, monsterObject.Monster.StatAbility);
        minihudObject.OnHide();

        // 상태 코루틴 시작
        checkStateAsync = CheckStateAsync();
        StartCoroutine(checkStateAsync);
    }

    private void Update()
    {
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
        }

        if (!hasArrived) // 도착 지점에 가지 않을 경우
        {
            // 도착 지점까지 갈 경우
            if (monsterObject.navMeshAgent.remainingDistance <= monsterObject.navMeshAgent.stoppingDistance)
            {
                hasArrived = true;

                monsterObject.OnIdle();

                // 상태 제어
                if (checkStateAsync != null)
                {
                    StopCoroutine(checkStateAsync);
                }

                checkStateAsync = CheckStateAsync();

                StartCoroutine(checkStateAsync);
            }
        }

        ControlMiniHUD(); // 미니 hud 제어
    }
    #region 상태 제어 관련
    private IEnumerator CheckStateAsync()
    {
        yield return new WaitForSeconds(Random.Range(monsterAIData.minSateTransitionTime, monsterAIData.maxStateTransitionTime));

        switch (Random.Range(0, 2))
        {
            case 0: OnIdle(); break;
            case 1: OnMove(); break;
        }
    }

    // 아이들
    private void OnIdle()
    {
        monsterObject.OnIdle();

        if (checkStateAsync != null)
        {
            StopCoroutine(checkStateAsync);
        }

        checkStateAsync = CheckStateAsync();

        StartCoroutine(checkStateAsync);
    }

    // 이동
    private void OnMove()
    {
        hasArrived = false;

        // 스폰 지점 기준으로 이동할 범위 계산
        var randPoint = Random.insideUnitSphere * monsterAIData.insideUnitSphere;
        randPoint.y = monsterObject.transform.position.y;

        var respos = randPoint + monsterObject.transform.position;

        // 네비 메쉬 지역안 기준으로 이동
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(respos + randPoint, out navHit, 1000, NavMesh.AllAreas))
        {
            monsterObject.OnMove(navHit.position);
        }
    }
    #endregion
    #region 미니 HUD 제어 관련
    public void ControlMiniHUD()
    {
        var offset = PlayerManager.Instance.PlayerViewModel.HeroObject.transform.position - monsterObject.transform.position;

        if (offset.sqrMagnitude < 80)
        {
            minihudObject.OnShow();
        }
        else
        {
            minihudObject.OnHide();
        }
    }
    #endregion
}