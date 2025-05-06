using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameSubject
{
    public GameViewModel GameViewModel { get; private set; }

    private static GameManager instance;
    public static GameManager Instance { get => instance ??= FindAnyObjectByType<GameManager>(); }

    [SerializeField] private LevelSpawnController levelSpawnController;

    private List<IGameObserver> observers;

    private bool isCombat; // 전투 상황인지 체크

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
            }
        }
    }

    public void GameStart()
    {
        levelSpawnController.create();

        TimeLineManager.Instance.CombatTimeLine.Play();

        CombatNotify(); // 전투 상황을 알림

        isCombat = true;
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
    #endregion
}
