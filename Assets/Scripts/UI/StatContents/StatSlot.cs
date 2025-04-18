using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatSlot : View
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private Button pluseBtn;

    private PlayerViewModel viewModel;
    private GameViewModel gameViewModel;

    private Stat.StatTypes statType;

    private TextInfo textInfo;

    public void Init(PlayerViewModel viewModel, Stat.StatTypes statType)
    {
        this.statType = statType;

        this.viewModel = viewModel;
        gameViewModel = GameManager.Instance.GameViewModel;
        gameViewModel.PropertyChanged += OnViewModelPropertyChanged;

        textInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<TextInfo>(nameof(TextInfo), 100001 + (int)statType);

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
        switch (GameManager.Instance.GameViewModel.LanguageType)
        {
            case GameViewModel.LanguageTypes.Kr:
                nameText.text = textInfo.NameKr;
                break;
            case GameViewModel.LanguageTypes.En:
                nameText.text = textInfo.NameEn;
                break;
        }

        switch (statType)
        {
            case Stat.StatTypes.MaxHp: valueText.text = viewModel.HeroObject.Hero.StatAbility.MaxHp.ToString(); break;
            case Stat.StatTypes.AttackDamage: valueText.text = viewModel.HeroObject.Hero.StatAbility.AttackDamage.ToString(); break;
            case Stat.StatTypes.AbilityPower: valueText.text = viewModel.HeroObject.Hero.StatAbility.AbilityPower.ToString(); break;
            case Stat.StatTypes.MaxSpeed: valueText.text = viewModel.HeroObject.Hero.StatAbility.MaxSpeed.ToString(); break;
        }

    }


    public void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        Debug.Log(e.PropertyName);
        if (e.PropertyName == "SetLanguage")
        {
            UpdateUI();
        }
    }
}
