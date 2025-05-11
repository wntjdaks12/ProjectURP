using Newtonsoft.Json;
using System;
using System.Collections.Generic;

// é�� ���� ����
public class ChapterRewardInfo : Data
{
    [JsonProperty]
    public IReadOnlyList<ChapterRewardItemInfo> ChapterRewardItemInfos { get; private set; } // é�� ���� ������ ����
}

// é�� ���� ������ ����
public class ChapterRewardItemInfo
{
    [JsonProperty]
    public int ItemId { get; private set; } // ������ ���̵�
    [JsonProperty]
    private int minCount; // ���� �ּ� ����
    [JsonProperty]
    private int maxCount; // ���� �ִ� ����

    public int GetRewardCount()
    {
        return UnityEngine.Random.Range(minCount, maxCount + 1);
    }
}
