using System.Collections.Generic;

public class DropItemInfo : Data
{
    public List<DropItemData> DropItemDatas { get; set; }
}

public class DropItemData
{
    public int DropId { get; set; }
    public int DropCount { get; set; }
    public int DropProbability { get; set; }
}