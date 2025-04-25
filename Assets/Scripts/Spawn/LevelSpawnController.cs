using System.Collections;
using UnityEngine;


public class LevelSpawnController : MonoBehaviour
{
    public int spawnEntityId;
    public int spawnRadius;
    public int spawnCount;

    public void create()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            var randPoint = Random.insideUnitSphere * spawnRadius; randPoint.y = 0;
            var spawnPoint = transform.position + randPoint;

            var monsterObj = GameApplication.Instance.EntityController.Spawn<Monster, MonsterObject>(spawnEntityId, spawnPoint, Quaternion.identity);
            var miniHUDObj = GameApplication.Instance.EntityController.Spawn<MiniHUD, MiniHUDObject>(110001, Camera.main.WorldToScreenPoint(monsterObj.MiniHUDNode.position), Quaternion.identity, UIManager.Instance.MiniHUDPanel);
            miniHUDObj.Init(monsterObj, monsterObj.Monster.StatAbility);

            GameApplication.Instance.EntityController.Spawn<VFX, VFXObject>(40005, monsterObj.transform.position, Quaternion.identity);
        }
    }
}
