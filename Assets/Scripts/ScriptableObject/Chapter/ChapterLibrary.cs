using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChapterLibrary", menuName = "Game/Chapter")]

public class ChapterLibrary : ScriptableObject
{
    [SerializeField] private  List<ChapterSpawnInfo> chpaterSpawnInfos;

    public ChapterSpawnInfo GetData(int chapterId, int stageIndex)
    {
        return chpaterSpawnInfos.Find(x => x.chapterId == chapterId && x.stageIndex == stageIndex);
    }

}

[System.Serializable]
public class ChapterSpawnInfo
{
    [field: SerializeField] public int chapterId { get; private set; }
    [field: SerializeField] public int stageIndex { get; private set; }
    [field: SerializeField] public MonsterSpawnData spawnData { get; private set; }
}