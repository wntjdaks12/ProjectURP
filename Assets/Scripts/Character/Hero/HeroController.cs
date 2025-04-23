using System.Linq;
using UnityEngine;

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

            CheckMp();
        }
    }

    #region 마나 관련 제어

    public void CheckMp()
    {
        if (heroObject.CheckFillMp()) // Mp가 가득 차 있을 경우
        {
            heroObject.OnConsumeMp(); // 마나 소모

            TimeLineManager.Instance.UltSkillEffectTimeLineDirector.Play();

            heroObject.UseSkill(50003);
        }
        else
        {
            OnRecoverMp(); // 마나 회복
        }
    }

    public void OnRecoverMp()
    {
        heroObject.OnRecoverMp(1);
    }
    #endregion
}