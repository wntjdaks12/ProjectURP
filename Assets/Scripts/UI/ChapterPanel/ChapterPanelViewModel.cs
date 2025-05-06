using System.Collections.Generic;
using System.Linq;

public class ChapterPanelViewModel : ViewModel
{
    private ChapterInfo chapterInfo;
    private List<TextInfo> textInfos;
    public IconInfo ChapterIconInfo { get; private set; }

    private ChapterViewModel chapterViewModel;

    public ChapterPanelViewModel()
    {
        chapterViewModel = ChapterManager.Instance.ChapterViewModel;

        chapterInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<ChapterInfo>(nameof(ChapterInfo), chapterViewModel.CurrentChapterId);

        textInfos = new List<TextInfo>();

        ChapterIconInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<IconInfo>(nameof(IconInfo), chapterViewModel.CurrentChapterId);

        textInfos.Add(GameApplication.Instance.GameModel.PresetData.ReturnData<TextInfo>(nameof(TextInfo), chapterInfo.Id));

        var count = chapterInfo.StageInfos.Count;
        for (int i = 0; i < count; i++)
        {
            textInfos.Add(GameApplication.Instance.GameModel.PresetData.ReturnData<TextInfo>(nameof(TextInfo), chapterInfo.StageInfos[i].StageId));
        }
    }

    public TextInfo GetCurrentChapterTextInfo()
    {
        return textInfos.Where(x => x.Id == chapterViewModel.CurrentChapterId).FirstOrDefault();
    }

    public TextInfo GetCurrentStageInfo()
    {
        return textInfos.Where(x => x.Id == chapterViewModel.CurrentStageId).FirstOrDefault();
    }
}
