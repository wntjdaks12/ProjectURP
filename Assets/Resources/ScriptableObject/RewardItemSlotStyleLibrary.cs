using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardItemSlotStyleLibrary", menuName = "UI/RewardItemSlotStyleLibrary")]
public class RewardItemSlotStyleLibrary : ScriptableObject
{
    public List<RewardItemSlotStyle> slotStyles;

    private Dictionary<Item.RarityTypes, RewardItemSlotStyle> styleDict;

    public void Initialize()
    {
        styleDict = new Dictionary<Item.RarityTypes, RewardItemSlotStyle>();
        foreach (var style in slotStyles)
        {
            styleDict[style.rarityType] = style;
        }
    }

    public RewardItemSlotStyle GetStyle(Item.RarityTypes grade)
    {
        if (styleDict == null)
            Initialize();
        return styleDict.TryGetValue(grade, out var style) ? style : null;
    }   
}
