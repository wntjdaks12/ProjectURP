using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class HeroController : MonoBehaviour
{
    public HeroObject heroObject;

    private void Update()
    {
        if (heroObject == null) return;

        var monsters = GameApplication.Instance.GameModel.RunTimeData.ReturnDatas<Monster>(nameof(Monster));

        var targetMonster = monsters.OrderBy(x => Vector3.Distance(x.Transform.position, heroObject.transform.position)).FirstOrDefault();

        if (targetMonster != null)
        {
            heroObject.OnMove(targetMonster.Transform.position);

            OnRecoverMp();
        }
    }

    #region 마나 관련 제어
    public void OnRecoverMp()
    {
        heroObject.OnRecoverMp(1);
    }
    #endregion
}