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
        var stageInfo = ChapterViewModel.GetStageInfo();

        if (stageInfo != null)
        {
            for (int i = 0; i < stageInfo.SpawnInfos.Count; i++)
            {
                for (int j = 0; j < stageInfo.SpawnInfos[i].Count; j++)
                {
                    var randPoint = Random.insideUnitSphere * 5; randPoint.y = 0;
                    var spawnPoint = new Vector3(9.55f, 6.937001f, 9.09f) + randPoint;

                    var monsterObj = GameApplication.Instance.EntityController.Spawn<Monster, MonsterObject>(stageInfo.SpawnInfos[i].SpawnId, spawnPoint, Quaternion.identity);
                    var miniHUDObj = GameApplication.Instance.EntityController.Spawn<MiniHUD, MiniHUDObject>(110001, Camera.main.WorldToScreenPoint(monsterObj.MiniHUDNode.position), Quaternion.identity, UIManager.Instance.MiniHUDPanel);
                    miniHUDObj.Init(monsterObj, monsterObj.Monster.StatAbility);

                    GameApplication.Instance.EntityController.Spawn<VFX, VFXObject>(40005, monsterObj.transform.position, Quaternion.identity);
                }
            }
        }
    }
}
