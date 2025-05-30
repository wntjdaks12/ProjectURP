using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class HeroController : MonoBehaviour, IGameObserver
{
    public HeroObject heroObject;

    private bool istemp;

    private bool isCombat; // 전투 상황인지 체크
    private bool isEndCombat; // 전투 끝인지 체크

    private Coroutine randomAsync;

    private MiniHUDObject miniHUDObj;

    private void OnEnable()
    {
        isCombat = false;
        isEndCombat = false;

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

        if (isEndCombat) return;

        if (!isCombat)
        {
            if (randomAsync == null) IdleNotify();
        }
        else
        {
            var monsterObjs = GameApplication.Instance.GameModel.RunTimeData.ReturnDatas<MonsterObject>(nameof(MonsterObject));

            if (monsterObjs != null)
            {
                if (!istemp)
                {
                    istemp = true;
                    heroObject.InitSkill();
                }

                var targetMonster = monsterObjs.OrderBy(x => Vector3.Distance(x.transform.position, heroObject.transform.position)).FirstOrDefault();

                if (monsterObjs.Length >= 1)
                {
                    heroObject.OnMove(targetMonster.transform.position);
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
    public void StartCombatNotify()
    {
        if (randomAsync != null)
        {
            StopCoroutine(randomAsync);
        }

        randomAsync = null;

        // 미니 HUD 생성
        miniHUDObj = GameApplication.Instance.EntityController.Spawn<MiniHUD, MiniHUDObject>(110002, Camera.main.WorldToScreenPoint(heroObject.MiniHUDNode.position), Quaternion.identity, UIManager.Instance.MiniHUDPanel);
        miniHUDObj.Init(heroObject, heroObject.Hero.StatAbility);

        isCombat = true;
    }

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

    public void EndCombatNotify()
    {
        if (randomAsync != null)
        {
            StopCoroutine(randomAsync);
        }

        randomAsync = null;

        heroObject.OnMove(heroObject.transform.position);

        miniHUDObj.Entity.OnRemoveData();

        isCombat = false;
        isEndCombat = true;
    }
    #endregion

    #region 마나 관련 제어
    public void CheckMp()
    {
        if (heroObject.CheckFillMp()) // Mp가 가득 차 있을 경우
        {
            heroObject.OnConsumeMp(); // 마나 소모

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