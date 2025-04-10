using UnityEngine;

public class GameApplication : MonoBehaviour
{
    private static GameApplication instance;
    public static GameApplication Instance { get => instance ??= FindObjectOfType<GameApplication>(); }

    private GameModel gameModel;
    public GameModel GameModel 
    {
        get => gameModel ??= FindObjectOfType<GameModel>();
    }

    private EntityController entityController;
    public EntityController EntityController
    {
        get => entityController ??= FindObjectOfType<EntityController>();
    }
    /*
    private ServerManager serverManager;
    public ServerManager ServerManager
    {
        get => serverManager ??= FindObjectOfType<ServerManager>();
    }

    private PlayerManager playerManager;
    public PlayerManager PlayerManager
    {
        get => playerManager ??= FindObjectOfType<PlayerManager>();
    }

    private StageManager stageManager;
    public StageManager StageManager
    {
        get => stageManager ??= FindObjectOfType<StageManager>();
    }*/

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        Application.targetFrameRate = 60;
    }
}
