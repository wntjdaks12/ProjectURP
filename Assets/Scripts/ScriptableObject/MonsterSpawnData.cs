using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSpawnData", menuName = "Game/Spawn/MonsterSpawn")]
public class MonsterSpawnData : ScriptableObject
{
    public List<SpawnDataInfo> spawnDataInfos = new List<SpawnDataInfo>();
}

[System.Serializable]
public class SpawnDataInfo
{
    public int monsterId;
    public Vector3 position;
}