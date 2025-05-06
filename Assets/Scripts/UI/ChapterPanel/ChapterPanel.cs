using System.Collections;
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

    private ChapterPanelViewModel viewModel;

    private void Start()
    {
        viewModel = new ChapterPanelViewModel();

        Init();
    }

    public void Init()
    {
        UpdateUI();

        StartCoroutine(StartTimeLineAsync());
    }

    private IEnumerator StartTimeLineAsync()
    {
        yield return new WaitForSeconds(0.5f);

        playableDirector?.Play();
    }

    #region UI 관련 업데이트
    public override void UpdateUI()
    {
        if (chapterNameText) chapterNameText.text = viewModel.GetCurrentChapterTextInfo()?.NameKr;
        if (stageNameText) stageNameText.text = viewModel.GetCurrentStageInfo()?.NameKr;
        if (chapterIconImage) chapterIconImage.sprite = Resources.Load<Sprite>(viewModel.ChapterIconInfo.Path);
    }
    #endregion
}
