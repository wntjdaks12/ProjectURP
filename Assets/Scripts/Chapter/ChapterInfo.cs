using System.Collections.Generic;

public class ChapterInfo : Data
{
    public List<StageInfo> StageInfos { get; set; }
}

public class StageInfo 
{
    public int StageId { get; set; }
    public List<int> MonsterIds { get; set; }
}
