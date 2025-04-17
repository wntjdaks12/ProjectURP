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
            var heroObject = PlayerManager.Instance.PlayerViewModel.HeroObject;

            var heroStatAbility = heroObject.Hero.StatAbility;
            var skillStatAbility = heroObject.SkillSystem.StatAbility;

            var textInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<TextInfo>(nameof(TextInfo), itemId).Clone() as TextInfo;
            textInfo.DescriptionEn = textInfo.DescriptionEn.GetStatReplace(new StatAbility[2] { skillStatAbility, heroStatAbility });

            var tooltipBox = Instantiate(Resources.Load<TooltipBox>("Prefabs/UI/TooltipBox/TooltipBox"));
            tooltipBox.Init(textInfo, transform.position, UIManager.Instance.OverrayCanvas.transform);
        });
    }
}