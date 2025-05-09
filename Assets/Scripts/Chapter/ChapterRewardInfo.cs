using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 챕터 보상 정보
public class ChapterRewardInfo : Data
{
    public List<ChapterRewardItemInfo> ChapterRewardItemInfos { get; set; } // 챕터 보상 아이템 정보
}

public class ChapterRewardItemInfo
{
    public int ItemId { get; set; } // 아이템 아이디
    public int MinCount { get; set; } // 보상 최소 개수
    public int MaxCount { get; set; } // 보상 최대 개수
}
