using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// é�� ���� ����
public class ChapterRewardInfo : Data
{
    public List<ChapterRewardItemInfo> ChapterRewardItemInfos { get; set; } // é�� ���� ������ ����
}

public class ChapterRewardItemInfo
{
    public int ItemId { get; set; } // ������ ���̵�
    public int MinCount { get; set; } // ���� �ּ� ����
    public int MaxCount { get; set; } // ���� �ִ� ����
}
