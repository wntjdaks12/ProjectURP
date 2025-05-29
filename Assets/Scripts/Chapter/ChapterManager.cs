using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    private static ChapterManager instance;
    public static ChapterManager Instance { get => instance ??= FindAnyObjectByType<ChapterManager>(); }

    public ChapterViewModel ChapterViewModel { get; private set; }

    private void Awake()
    {
        ChapterViewModel = new ChapterViewModel();
    }

    public void StartCurrentStage()
    {
        if (ChapterViewModel.ExistCurrentStage())
        {
            Spawn();
        }
    }

    public void StartNextStage()
    {
        if (ChapterViewModel.ExistNextStage())
        {
            ChapterViewModel.NextStage();
        }
    }

    // 나중에 코드 위치 수정 스폰
    private void Spawn()
    {
        var chapterSpawnInfo = ChapterViewModel.GetChapterSpawnInfo();

        var spawnData = chapterSpawnInfo.spawnData;

        for (int i = 0; i < spawnData.spawnDataInfos.Count; i++)
        {
            var spawnDataInfo = spawnData.spawnDataInfos[i];

            GameApplication.Instance.EntityController.Spawn<Monster, MonsterObject>(spawnDataInfo.monsterId, spawnDataInfo.position, Quaternion.identity);
        }
    }
}
