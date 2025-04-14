using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        var palyerManager = PlayerManager.Instance;
        palyerManager.PlayerViewModel.HeroObject = GameApplication.Instance.EntityController.Spawn<Hero, HeroObject>(palyerManager.PlayerViewModel.HeroId, Vector3.zero, Quaternion.identity);
    }
}
