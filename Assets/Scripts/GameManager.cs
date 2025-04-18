using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameViewModel GameViewModel { get; private set; }

    private static GameManager instance;
    public static GameManager Instance { get => instance ??= FindAnyObjectByType<GameManager>(); }

    private void Awake()
    {
        GameViewModel = new GameViewModel();
    }

    private void Start()
    {
        var palyerManager = PlayerManager.Instance;
        palyerManager.PlayerViewModel.HeroObject = GameApplication.Instance.EntityController.Spawn<Hero, HeroObject>(palyerManager.PlayerViewModel.HeroId, Vector3.zero, Quaternion.identity);

        GameViewModel.SetLanguage(GameViewModel.LanguageTypes.Kr);
    }
}
