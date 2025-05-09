using Newtonsoft.Json;
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
    public int MinCount { get; private set; } // ���� �ּ� ����
    [JsonProperty]
    public int MaxCount { get; private set; } // ���� �ִ� ����
}
