using Newtonsoft.Json;
using System;
using System.Collections.Generic;

// 챕터 보상 정보
public class ChapterRewardInfo : Data
{
    [JsonProperty]
    public IReadOnlyList<ChapterRewardItemInfo> ChapterRewardItemInfos { get; private set; } // 챕터 보상 아이템 정보
}

// 챕터 보상 아이템 정보
public class ChapterRewardItemInfo
{
    [JsonProperty]
    public int ItemId { get; private set; } // 아이템 아이디
    [JsonProperty]
    private int minCount; // 보상 최소 개수
    [JsonProperty]
    private int maxCount; // 보상 최대 개수

    public int GetRewardCount()
    {
        return UnityEngine.Random.Range(minCount, maxCount + 1);
    }
}
