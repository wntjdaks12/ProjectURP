using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public MonsterObject monsterObject;

    private Transform target;

    private bool hasArrived; // ���� ������ �ִ���

    private IEnumerator checkStateAsync;

    private MonsterAIData monsterAIData; // ���� ai ������

    private MiniHUDObject minihudObject;

    private void Awake()
    {
        Debug.Assert(monsterObject != null, "���� ������Ʈ ����");

        monsterAIData = Resources.Load<MonsterAIData>("ScriptableObject/MonsterAI/FiledMonsterAIData");
        Debug.Assert(monsterAIData != null, "���� AI ������ Null");
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

        // ���� �ڷ�ƾ ����
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

        if (!hasArrived) // ���� ������ ���� ���� ���
        {
            // ���� �������� �� ���
            if (monsterObject.navMeshAgent.remainingDistance <= monsterObject.navMeshAgent.stoppingDistance)
            {
                hasArrived = true;

                monsterObject.OnIdle();

                // ���� ����
                if (checkStateAsync != null)
                {
                    StopCoroutine(checkStateAsync);
                }

                checkStateAsync = CheckStateAsync();

                StartCoroutine(checkStateAsync);
            }
        }

        ControlMiniHUD(); // �̴� hud ����
    }
    #region ���� ���� ����
    private IEnumerator CheckStateAsync()
    {
        yield return new WaitForSeconds(Random.Range(monsterAIData.minSateTransitionTime, monsterAIData.maxStateTransitionTime));

        switch (Random.Range(0, 2))
        {
            case 0: OnIdle(); break;
            case 1: OnMove(); break;
        }
    }

    // ���̵�
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

    // �̵�
    private void OnMove()
    {
        hasArrived = false;

        // ���� ���� �������� �̵��� ���� ���
        var randPoint = Random.insideUnitSphere * monsterAIData.insideUnitSphere;
        randPoint.y = monsterObject.transform.position.y;

        var respos = randPoint + monsterObject.transform.position;

        // �׺� �޽� ������ �������� �̵�
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(respos + randPoint, out navHit, 1000, NavMesh.AllAreas))
        {
            monsterObject.OnMove(navHit.position);
        }
    }
    #endregion
    #region �̴� HUD ���� ����
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