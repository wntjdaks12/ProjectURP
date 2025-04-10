using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        foreach (var heroInfo in PlayerManager.Instance.heroInfos)
        {
            var obj = GameApplication.Instance.EntityController.Spawn<Hero, HeroObject>(heroInfo.heroId, Vector3.zero, Quaternion.identity);

            var skill = new SkillSystem(50001, obj);
        }
    }
}
