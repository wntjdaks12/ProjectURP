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

    #region ���� ���� ����

    public void CheckMp()
    {
        if (heroObject.CheckFillMp()) // Mp�� ���� �� ���� ���
        {
            heroObject.OnConsumeMp(); // ���� �Ҹ�

            TimeLineManager.Instance.UltSkillEffectTimeLineDirector.Play();

            heroObject.UseSkill(50003);
        }
        else
        {
            OnRecoverMp(); // ���� ȸ��
        }
    }

    public void OnRecoverMp()
    {
        heroObject.OnRecoverMp(1);
    }
    #endregion
}