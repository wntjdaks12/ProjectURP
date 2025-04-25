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
    {/*
        var palyerManager = PlayerManager.Instance;
        palyerManager.PlayerViewModel.HeroObject = GameApplication.Instance.EntityController.Spawn<Hero, HeroObject>(palyerManager.PlayerViewModel.HeroId, Vector3.zero, Quaternion.identity);
        
        var heroObj = palyerManager.PlayerViewModel.HeroObject;

        var miniHUDObj = GameApplication.Instance.EntityController.Spawn<MiniHUD, MiniHUDObject>(110002, Camera.main.WorldToScreenPoint(heroObj.MiniHUDNode.position), Quaternion.identity, UIManager.Instance.MiniHUDPanel);
        miniHUDObj.Init(heroObj, heroObj.Hero.StatAbility);*/

        GameViewModel.SetLanguage(GameViewModel.LanguageTypes.Kr);
    }

    public void GameStart()
    {
        levelSpawnController.create();
    }
}
