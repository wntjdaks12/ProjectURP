    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    public GameManager gameManager;
    public SpawnManager spawnManager;

    void Awake()
    {
        Debug.Assert(gameManager != null, "GameManager Null");
        Debug.Assert(spawnManager != null, "SpawnManager Null");

        gameManager.Init(spawnManager);
    }
}
