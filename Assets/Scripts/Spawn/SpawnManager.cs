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

                // ����� ����
                var heroData = PlayerManager.Instance.PlayerViewModel.HeroDatas[0];
                var heroObj = GameApplication.Instance.EntityController.Spawn<Hero, HeroObject>(heroData.id, position, Quaternion.identity);
                PlayerManager.Instance.PlayerViewModel.HeroObject = heroObj;

                // ��ȯ VFX ����
                GameApplication.Instance.EntityController.Spawn<VFX, VFXObject>(40005, heroObj.transform.position, Quaternion.identity);

                OnSpawned?.Invoke(heroObj.gameObject);

                break;
            case SpawnType.Monster:
                break;
        }
    }
}
