using System.Collections.Generic;

public class ChapterInfo : Data
{
    public List<StageInfo> StageInfos { get; set; }
}

public class StageInfo 
{
    public int Index { get; set; }
    public int StageId { get; set; }
    public List<SpawnInfo> SpawnInfos { get; set; }
}

public class SpawnInfo
{
    public int SpawnId { get; set; }
    public int Count { get; set; }
}
