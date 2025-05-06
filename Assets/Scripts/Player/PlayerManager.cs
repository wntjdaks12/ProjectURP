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
        PlayerViewModel.AddHero(10001); // �ӽ÷� �߰� (���߿� �÷��̾� �����ͷ�)
        PlayerViewModel.AddHero(10002); // �ӽ÷� �߰� (���߿� �÷��̾� �����ͷ�)
    }
}