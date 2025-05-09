using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class ChapterViewModel : ViewModel
{
    private ChapterInfo chapterInfo; // 챕터 정보
    public ChapterRewardInfo ChapterRewardInfo { get; private set; } // 챕터 보상 정보

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

    public List<TextInfo> RewardItemTextInfos { get; private set; }
    public List<IconInfo> RewardItemIconInfos { get; private set; }

    public ChapterViewModel()
    {
        CurrentChapterId = 130001; // 임시로 추가 (나중에 플레이어 데이터로)
        CurrentStageId = 130002; // 임시로 추가 (나중에 플레이어 데이터로)
        CurrentStageIndex = 0;

        chapterInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<ChapterInfo>(nameof(ChapterInfo), CurrentChapterId); // 챕터 정보 가져오기
        ChapterRewardInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<ChapterRewardInfo>(nameof(ChapterRewardInfo), CurrentChapterId); // 챕터 보상 정보 가져오기

        if (ChapterRewardInfo != null && ChapterRewardInfo.ChapterRewardItemInfos != null)
        {
            RewardItemTextInfos = new List<TextInfo>();
            RewardItemIconInfos = new List<IconInfo>();

            for (int i = 0; i < ChapterRewardInfo.ChapterRewardItemInfos.Count; i++)
            {
                var itemId = ChapterRewardInfo.ChapterRewardItemInfos[i].ItemId;
                RewardItemTextInfos.Add(GameApplication.Instance.GameModel.PresetData.ReturnData<TextInfo>(nameof(TextInfo), itemId));
                RewardItemIconInfos.Add(GameApplication.Instance.GameModel.PresetData.ReturnData<IconInfo>(nameof(IconInfo), itemId));
            }
        }
    }

    public bool ExistCurrentStage()
    {
        if (chapterInfo.StageInfos.Count > CurrentStageIndex)
        {
            Debug.Log("현재 스테이지는 존재 여부 (0)");
            return true;
        }
        else
        {
            Debug.Log("현재 스테이지는 존재 여부 (X)");
            return false;
        }
    }

    public bool ExistNextStage()
    {
        if (chapterInfo.StageInfos.Count > CurrentStageIndex + 1)
        {
            Debug.Log("다음 스테이지는 존재 여부 (0)");
            return true;
        }
        else
        {
            Debug.Log("다음 스테이지는 존재 여부 (X)");
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

                Debug.Log($"{currentChapterId}챕터의 {currentStageId}스테이지 진입");
            }
        }
    }

    public StageInfo GetStageInfo()
    {
        return chapterInfo.StageInfos.Where(x => x.Index == CurrentStageIndex).FirstOrDefault();
    }
}
