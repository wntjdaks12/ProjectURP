using System.Collections;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ChapterPanel : View
{
    [SerializeField] private TextMeshProUGUI chapterNameText;
    [SerializeField] private TextMeshProUGUI stageNameText;
    [SerializeField] private Image chapterIconImage;

    [SerializeField] private PlayableDirector playableDirector;

    private ChapterViewModel chapterViewModel;

    private void OnEnable()
    {
        chapterViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public void Init()
    {
        if(chapterViewModel == null) chapterViewModel = ChapterManager.Instance.ChapterViewModel;

        OnShow();

        UpdateUI();

        playableDirector?.Play();
    }

    private void OnDisable()
    {
        chapterViewModel.PropertyChanged -= OnViewModelPropertyChanged;
    }

    #region UI 관련 업데이트
    public void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "CurrentStageId")
        {
            UpdateUI();

            playableDirector?.Play();
        }
    }

    public override void UpdateUI()
    {
        if (chapterNameText) chapterNameText.text = chapterViewModel.ChapterTextInfo?.NameKr;
        if (stageNameText) stageNameText.text = chapterViewModel.StageTextInfo?.NameKr;
        if (chapterIconImage) chapterIconImage.sprite = Resources.Load<Sprite>(chapterViewModel.ChapterIconInfo.Path);
    }
    #endregion
}
