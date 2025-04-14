using System;
using System.Collections.Generic;
using System.Linq;

public class StatData : Data
{
    public List<Stat> Stats { get; set; }

    public float GetTotalStatValue(Stat.StatTypes statType)
    {
        return Stats.Where(x => x.StatType == statType).Sum(x => x.Value);
    }

    public void UpdateStatValue(Stat.StatTypes statType, float value)
    {
        var stat = Stats.Where(x => x.StatType == statType).FirstOrDefault();

        stat.Value += value;
    }
}
