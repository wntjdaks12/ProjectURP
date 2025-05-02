using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SkillInfoContents : View
{
    [SerializeField] private TextMeshProUGUI skillNameText;
    [SerializeField] private TextMeshProUGUI skillDescriptionText;
    [SerializeField] private Image skillIconImage;

    private CharacterMenuViewModel viewModel;

    public void Init(CharacterMenuViewModel viewModel)
    {
        this.viewModel = viewModel;

        UpdateUI();
    }

    #region UI 업데이트 관련
    public override void UpdateUI()
    {
        var heroData = viewModel.GetHeroData();

        if (heroData != null)
        {
            if (skillNameText != null) skillNameText.text = heroData.skillTextInfo.NameKr;
        }
    }
    #endregion
}