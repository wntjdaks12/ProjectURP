using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance { get => instance ??= FindAnyObjectByType<PlayerManager>(); }

    public PlayerViewModel PlayerViewModel { get; private set; }

    private void Awake()
    {
        PlayerViewModel = new PlayerViewModel();
        PlayerViewModel.AddHero(10001); // 임시로 추가 (나중에 플레이어 데이터로)
        PlayerViewModel.AddHero(10002); // 임시로 추가 (나중에 플레이어 데이터로)
    }
}