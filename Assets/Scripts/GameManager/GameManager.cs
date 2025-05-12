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

    private bool isCombat; // 전투 상황인지 체크

    public UnityEvent OnChapterClearEvent;

    private void Awake()
    {
        GameViewModel = new GameViewModel();

        observers = new List<IGameObserver>();
    }

    private void Start()
    {
        GameViewModel.SetLanguage(GameViewModel.LanguageTypes.Kr);
    }

    private void Update()
    {
        if (isCombat)
        {
            if (GameApplication.Instance.GameModel.RunTimeData.ReturnDatas<MonsterObject>(nameof(MonsterObject)).Length <= 0)
            {
                IdleNotify(); // 무방비 상황을 알림

                isCombat = false;

                if (ChapterManager.Instance.ChapterViewModel.ExistNextStage())
                    StartCoroutine(CombatDelayAsync());
                else
                {
                    EndCombat(); // 전투 끝나는 상황을 알림

                    OnChapterClearEvent?.Invoke();
                }
            }
        }
    }

    // 전투 시작전 딜레이
    private IEnumerator CombatDelayAsync()
    {
        ChapterManager.Instance.StartNextStage();

        yield return new WaitForSeconds(3f);

        RestartCombat();
    }

    // 전투 시작
    public void StartCombat()
    {
        ChapterManager.Instance.StartCurrentStage();

        TimeLineManager.Instance.CombatTimeLine.Play();

        StartCombatNotify(); // 전투 시작 상황을 알림

        isCombat = true;
    }

    // 전투 다시 시작
    private void RestartCombat()
    {
        ChapterManager.Instance.StartCurrentStage();

        CombatNotify(); // 전투 상황을 알림

        isCombat = true;
    }

    // 전투 종료
    public void EndCombat()
    {
        EndCombatNotify(); // 전투 종료 알림

        isCombat = false;
    }

    #region 옵저버 패턴
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
