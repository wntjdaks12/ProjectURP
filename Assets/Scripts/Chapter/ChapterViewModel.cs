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
        CurrentChapterId = 130001; // �ӽ÷� �߰� (���߿� �÷��̾� �����ͷ�)
        CurrentStageId = 130002; // �ӽ÷� �߰� (���߿� �÷��̾� �����ͷ�)
    }
}
