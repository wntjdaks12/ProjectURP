using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameViewModel GameViewModel { get; private set; }

    private static GameManager instance;
    public static GameManager Instance { get => instance ??= FindAnyObjectByType<GameManager>(); }

    [SerializeField] private LevelSpawnController levelSpawnController;

    private void Awake()
    {
        GameViewModel = new GameViewModel();
    }

    private void Start()
    {
        GameViewModel.SetLanguage(GameViewModel.LanguageTypes.Kr);
    }

    public void GameStart()
    {
        levelSpawnController.create();

        TimeLineManager.Instance.CombatTimeLine.Play();
    }
}
