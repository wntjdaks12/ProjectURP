using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    private static ChapterManager instance;
    public static ChapterManager Instance { get => instance ??= FindAnyObjectByType<ChapterManager>(); }

    public ChapterViewModel ChapterViewModel { get; private set; }

    private List<bool> isExitMonsters;

    private ChapterSpawnInfo chapterSpawnInfo;

    private void Awake()
    {
        ChapterViewModel = new ChapterViewModel();

        isExitMonsters = new List<bool>();
    }

    private void Start()
    {

        StartCoroutine(asdAsync());
    }

    private void Update()
    {
    }

    public void StartCurrentStage()
    {
        if (ChapterViewModel.ExistCurrentStage())
        {
            //Spawn();
        }
    }

    public void StartNextStage()
    {
        if (ChapterViewModel.ExistNextStage())
        {
            ChapterViewModel.NextStage();
        }
    }

    private IEnumerator asdAsync()
    {
        chapterSpawnInfo = ChapterViewModel.GetChapterSpawnInfo();
        var spawnData = chapterSpawnInfo.spawnData;

        for (int i = 0; i < spawnData.spawnDataInfos.Count; i++)
        {
            isExitMonsters.Add(false);
        }

        while (true)
        {
            for (int i = 0; i < isExitMonsters.Count; i++)
            {
                if (!isExitMonsters[i])
                {
                    var spawnDataInfo = spawnData.spawnDataInfos[i];

                    var monsterObj = GameApplication.Instance.EntityController.Spawn<Monster, MonsterObject>(spawnDataInfo.monsterId, spawnDataInfo.position, Quaternion.identity);

                    var index = i;
                    monsterObj.Entity.OnDataRemove += (data) =>
                    {
                        isExitMonsters[index] = false;
                    };

                    isExitMonsters[i] = true;
                }
            }

            yield return new WaitForSeconds(3f);
        }
    }
}
