using System.ComponentModel;
using UnityEngine;

public class PlayerHUDItemSlot : ItemSlot
{
    public override void Init(int itemId)
    {
        base.Init(itemId);

        slotBtn.onClick.RemoveAllListeners();
        slotBtn.onClick.AddListener(() =>
        {
            var item = GameApplication.Instance.GameModel.PresetData.ReturnData<Item>(nameof(Item), itemId);

            var heroObject = PlayerManager.Instance.PlayerViewModel.HeroObject;

            var heroStatAbility = heroObject.Hero.StatAbility;
            var skillSystem = heroObject.GetSkill(item.SkillId);

            var textInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<TextInfo>(nameof(TextInfo), itemId).Clone() as TextInfo;
            textInfo.DescriptionKr = textInfo.DescriptionKr.GetStatReplace(heroStatAbility, skillSystem);
            textInfo.DescriptionEn = textInfo.DescriptionEn.GetStatReplace(heroStatAbility, skillSystem);
            
            var tooltipBox = Instantiate(Resources.Load<TooltipBox>("Prefabs/UI/TooltipBox/TooltipBox"));
            tooltipBox.Init(textInfo, transform.position, UIManager.Instance.OverrayCanvas.transform);
        });
    }
}