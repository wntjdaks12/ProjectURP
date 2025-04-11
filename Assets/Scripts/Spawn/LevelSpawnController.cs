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

            GameApplication.Instance.EntityController.Spawn<Monster, MonsterObject>(spawnEntityId, spawnPoint, Quaternion.identity);
        }
    }
}
