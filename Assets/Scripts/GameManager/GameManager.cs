using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour, IGameSubject
{
    public GameViewModel GameViewModel { get; private set; }

    private static GameManager instance;
    public static GameManager Instance { get => instance ??= FindAnyObjectByType<GameManager>(); }

    private List<IGameObserver> observers;

    private bool isCombat; // ���� ��Ȳ���� üũ

    public UnityEvent OnStartCombatEvent;
    public UnityEvent OnRestartCombatEvent;
    public UnityEvent OnChapterClearEvent;

    private SpawnManager spawnManager;
    private CameraManager cameraManager;

    private void Awake()
    {
        GameViewModel = new GameViewModel();

        observers = new List<IGameObserver>();
    }

    public void Init(SpawnManager spawnManager, CameraManager cameraManager)
    {
        this.spawnManager = spawnManager;
        this.cameraManager = cameraManager;

        spawnManager.OnSpawned += (gameObject) => cameraManager.SwitchCamera(CameraManager.CameraTypes.Default, gameObject.transform);
    }

    private void Start()
    {
        GameViewModel.SetLanguage(GameViewModel.LanguageTypes.Kr);

        spawnManager.SpawnEntity(SpawnType.Player, new Vector3(0, 6.937001f, 0));

        StartCombat();
    }

    private void Update()
    {/*
        if (isCombat)
        {
            if (GameApplication.Instance.GameModel.RunTimeData.ReturnDatas<MonsterObject>(nameof(MonsterObject)).Length <= 0)
            {
                IdleNotify(); // ����� ��Ȳ�� �˸�

                isCombat = false;

                if (ChapterManager.Instance.ChapterViewModel.ExistNextStage())
                    StartCoroutine(CombatDelayAsync());
                else
                {
                    EndCombat(); // ���� ������ ��Ȳ�� �˸�

                    OnChapterClearEvent?.Invoke();
                }
            }
        }*/
    }

    // ���� ������ ������
    private IEnumerator CombatDelayAsync()
    {
        ChapterManager.Instance.StartNextStage();

        yield return new WaitForSeconds(3f);

        RestartCombat();
    }

    // ���� ����
    public void StartCombat()
    {
        ChapterManager.Instance.StartCurrentStage();

        TimeLineManager.Instance.CombatTimeLine.Play();

        StartCombatNotify(); // ���� ���� ��Ȳ�� �˸�

        isCombat = true;

        OnStartCombatEvent?.Invoke();
    }

    // ���� �ٽ� ����
    private void RestartCombat()
    {
        ChapterManager.Instance.StartCurrentStage();

        CombatNotify(); // ���� ��Ȳ�� �˸�

        isCombat = true;

        OnRestartCombatEvent?.Invoke();
    }

    // ���� ����
    public void EndCombat()
    {
        EndCombatNotify(); // ���� ���� �˸�

        isCombat = false;
    }

    #region ������ ����
    public void Register(IGameObserver observer)
    {
        observers.Add(observer);
    }

    public void Remove(IGameObserver observer)
    {
        observers.Remove(observer);
    }

    public void StartCombatNotify()
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].StartCombatNotify();
        }
    }

    public void IdleNotify()
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].IdleNotify();
        }
    }

    public void CombatNotify()
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].CombatNotify();
        }
    }

    public void EndCombatNotify()
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].EndCombatNotify();
        }
    }
    #endregion
}
