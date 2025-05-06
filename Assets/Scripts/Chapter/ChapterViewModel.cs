using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterViewModel : ViewModel
{
    private int currentChapterId;
    public int CurrentChapterId 
    {
        get { return currentChapterId; }
        set
        {
            if (currentChapterId != value)
            {
                currentChapterId = value;

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

                OnPropertyChanged();
            }
        }
    }

    public ChapterViewModel()
    {
        CurrentChapterId = 130001; // 임시로 추가 (나중에 플레이어 데이터로)
        CurrentStageId = 130002; // 임시로 추가 (나중에 플레이어 데이터로)
    }
}
