using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatSlot : View
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private Button pluseBtn;

    private PlayerViewModel viewModel;

    private Stat.StatTypes statType;

    public void Init(PlayerViewModel viewModel, Stat.StatTypes statType)
    {
        this.statType = statType;

        this.viewModel = viewModel;

        pluseBtn.onClick.RemoveAllListeners();
        pluseBtn.onClick.AddListener(() =>
        {
            viewModel.HeroObject.Hero.StatAbility.UpdateStatData(StatAbility.StatInfo.StatDataType.Sub, statType, 1);

            UpdateUI();
        });

        UpdateUI();
    }

    public override void UpdateUI()
    {
        nameText.text = Enum.GetName(typeof(Stat.StatTypes), statType);

        switch (statType)
        {
            case Stat.StatTypes.MaxHp: valueText.text = viewModel.HeroObject.Hero.StatAbility.MaxHp.ToString(); break;
            case Stat.StatTypes.AttackDamage: valueText.text = viewModel.HeroObject.Hero.StatAbility.AttackDamage.ToString(); break;
            case Stat.StatTypes.AbilityPower: valueText.text = viewModel.HeroObject.Hero.StatAbility.AbilityPower.ToString(); break;
            case Stat.StatTypes.MaxSpeed: valueText.text = viewModel.HeroObject.Hero.StatAbility.MaxSpeed.ToString(); break;
        }

    }
}
