using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class HeroController : MonoBehaviour, IGameObserver
{
    public HeroObject heroObject;

    private bool istemp;

    private bool isCombat; // ���� ��Ȳ���� üũ
    private bool isEndCombat; // ���� ������ üũ

    private MiniHUDObject miniHUDObj;

    private Joystick joystick;

    private void Awake()
    {
        joystick = FindObjectOfType<Joystick>();

        Debug.Assert(joystick != null, "���̽�ƽ ���� x");
    }

    private void OnEnable()
    {
        isCombat = false;
        isEndCombat = false;

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

        if (isEndCombat) return;

        heroObject.OnDirecMove(joystick.InputDirection);
        
        if (!isCombat)
        {
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
    public void StartCombatNotify()
    {
        // �̴� HUD ����
        miniHUDObj = GameApplication.Instance.EntityController.Spawn<MiniHUD, MiniHUDObject>(110002, Camera.main.WorldToScreenPoint(heroObject.MiniHUDNode.position), Quaternion.identity, UIManager.Instance.MiniHUDPanel);
        miniHUDObj.Init(heroObject, heroObject.Hero.StatAbility);

        isCombat = true;
    }

    public void IdleNotify()
    {
        isCombat = false;
    }

    public void CombatNotify()
    {
        isCombat = true;
    }

    public void EndCombatNotify()
    {
        heroObject.OnMove(heroObject.transform.position);

        miniHUDObj.Entity.OnRemoveData();

        isCombat = false;
        isEndCombat = true;
    }
    #endregion

    #region ���� ���� ����
    public void CheckMp()
    {
        if (heroObject.CheckFillMp()) // Mp�� ���� �� ���� ���
        {
            heroObject.OnConsumeMp(); // ���� �Ҹ�

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