using System;
using UnityEngine;

public enum SpawnType { Player, Monster }

public class SpawnManager : MonoBehaviour
{
    public event Action<GameObject> OnSpawned;

    public void SpawnEntity(SpawnType type, Vector3 position)
    {
        switch (type)
        {
            case SpawnType.Player:

                // 洒绢肺 积己
                var heroData = PlayerManager.Instance.PlayerViewModel.HeroDatas[0];
                var heroObj = GameApplication.Instance.EntityController.Spawn<Hero, HeroObject>(heroData.id, position, Quaternion.identity);
                PlayerManager.Instance.PlayerViewModel.HeroObject = heroObj;

                // 家券 VFX 积己
                GameApplication.Instance.EntityController.Spawn<VFX, VFXObject>(40005, heroObj.transform.position, Quaternion.identity);

                OnSpawned?.Invoke(heroObj.gameObject);

                break;
            case SpawnType.Monster:
                break;
        }
    }
}
