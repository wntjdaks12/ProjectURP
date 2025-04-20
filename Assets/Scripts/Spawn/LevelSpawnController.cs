using System.Collections;
using UnityEngine;


public class LevelSpawnController : MonoBehaviour
{
    public int spawnEntityId;
    public int spawnRadius;
    public int spawnCount;

    private void Start()
    {
        StartCoroutine(asdAsync());
    }

    private IEnumerator asdAsync()
    {
        while (true)
        {
            create();

            yield return new WaitForSeconds(5f);
        }
    }

    public void create()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            var randPoint = Random.insideUnitSphere * spawnRadius; randPoint.y = 0;
            var spawnPoint = transform.position + randPoint;

            var monsterObj = GameApplication.Instance.EntityController.Spawn<Monster, MonsterObject>(spawnEntityId, spawnPoint, Quaternion.identity);
            var miniHUDObj = GameApplication.Instance.EntityController.Spawn<MiniHUD, MiniHUDObject>(110001, Camera.main.WorldToScreenPoint(monsterObj.MiniHUDNode.position), Quaternion.identity, UIManager.Instance.MiniHUDPanel);
            miniHUDObj.Init(monsterObj, monsterObj.Monster.StatAbility);
        }
    }
}
