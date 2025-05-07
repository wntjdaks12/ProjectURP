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

    private Coroutine startTimeLineAsync;

    private void Start()
    {
        chapterViewModel = ChapterManager.Instance.ChapterViewModel;

        Init();

        chapterViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public void Init()
    {
        UpdateUI();

        StartTimeLine(0.5f);
    }

    #region 타임라인
    public void StartTimeLine(float initDelay)
    {
        if (startTimeLineAsync != null)
        {
            StopCoroutine(startTimeLineAsync);
        }

        startTimeLineAsync = StartCoroutine(StartTimeLineAsync(initDelay));
    }

    private IEnumerator StartTimeLineAsync(float initDelay)
    {
        yield return new WaitForSeconds(initDelay);

        playableDirector?.Play();
    }
    #endregion

    #region UI 관련 업데이트
    public void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "CurrentStageId")
        {
            UpdateUI();

            StartTimeLine(0f);
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
