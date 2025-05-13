using UnityEngine;

// ���� ���� ������ ���ε��ϴ� �ν��緯
public class GameInstaller : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private CameraManager cameraManager;

    void Awake()
    {
        Debug.Assert(gameManager != null, "GameManager Null");
        Debug.Assert(spawnManager != null, "SpawnManager Null");
        Debug.Assert(cameraManager != null, "CameraManager Null");

        gameManager.Init(spawnManager, cameraManager);
    }
}
