using UnityEngine;
using System.Linq;

public class ChapterViewModel : ViewModel
{
    private ChapterInfo chapterInfo;

    private int currentChapterId;
    public int CurrentChapterId 
    {
        get { return currentChapterId; }
        set
        {
            if (currentChapterId != value)
            {
                currentChapterId = value;

                ChapterTextInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<TextInfo>(nameof(TextInfo), CurrentChapterId);
                ChapterIconInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<IconInfo>(nameof(IconInfo), CurrentChapterId);

                OnPropertyChanged();
            }
        }
    }

    private int currentStageId;
    public int CurrentStageId 
    {
        get { return currentStageId; }
        set
        {
            if (currentStageId != value)
            {
                currentStageId = value;

                StageTextInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<TextInfo>(nameof(TextInfo), CurrentStageId);

                OnPropertyChanged();
            }
        }
    }

    public int CurrentStageIndex { get; private set; }

    public TextInfo ChapterTextInfo { get; private set; }
    public TextInfo StageTextInfo { get; private set; }
    public IconInfo ChapterIconInfo { get; private set; }

    public ChapterViewModel()
    {
        CurrentChapterId = 130001; // �ӽ÷� �߰� (���߿� �÷��̾� �����ͷ�)
        CurrentStageId = 130002; // �ӽ÷� �߰� (���߿� �÷��̾� �����ͷ�)
        CurrentStageIndex = 0;

        chapterInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<ChapterInfo>(nameof(ChapterInfo), CurrentChapterId);
    }

    public bool ExistCurrentStage()
    {
        if (chapterInfo.StageInfos.Count > CurrentStageIndex)
        {
            Debug.Log("���� ���������� ���� ���� (0)");
            return true;
        }
        else
        {
            Debug.Log("���� ���������� ���� ���� (X)");
            return false;
        }
    }

    public bool ExistNextStage()
    {
        if (chapterInfo.StageInfos.Count > CurrentStageIndex + 1)
        {
            Debug.Log("���� ���������� ���� ���� (0)");
            return true;
        }
        else
        {
            Debug.Log("���� ���������� ���� ���� (X)");
            return false;
        }
    }


    public void NextStage()
    {
        if (ExistNextStage())
        {
            var index = CurrentStageIndex + 1;

            var stageInfo = chapterInfo.StageInfos.Where(x => x.Index == index).FirstOrDefault();

            if (stageInfo != null)
            {
                CurrentStageIndex = stageInfo.Index;
                CurrentStageId = stageInfo.StageId;

                Debug.Log($"{currentChapterId}é���� {currentStageId}�������� ����");
            }
        }
    }

    public StageInfo GetStageInfo()
    {
        return chapterInfo.StageInfos.Where(x => x.Index == CurrentStageIndex).FirstOrDefault();
    }
}
