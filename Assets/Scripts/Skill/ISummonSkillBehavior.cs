using UnityEngine;

public class ISummonSkillBehavior : ISkillBehavior
{
    public SkillSystem SkillSystem { get; set; }

    private SummonSkillDetailInfo summonSkillDetailInfo;

    public ISummonSkillBehavior(SkillSystem skillSystem)
    {
        SkillSystem = skillSystem;

        // 소환 스킬 개체 디테일 정보 추가
        summonSkillDetailInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<SummonSkillDetailInfo>(nameof(SummonSkillDetailInfo), skillSystem.Id);
    }

    public void Use(CharacterObject caster)
    {
        if (caster == null) return;

        var skillObj = GameApplication.Instance.EntityController.Spawn<Skill, SkillObject>(summonSkillDetailInfo.SummonSkillId, Vector3.zero, Quaternion.identity, caster.transform);
        skillObj.SkillSystem = SkillSystem;
    }
}
