using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance { get => instance ??= FindAnyObjectByType<PlayerManager>(); }

    public List<HeroInfo> heroInfos;
}

[Serializable]
public class HeroInfo
{
    public int heroId;
    public List<int> itemIds;
}
