using UnityEngine.UI;
using System;
using UnityEngine;

public class ItemStoreSlot : View
{
    [SerializeField] private Image IconImg;
    [SerializeField] private Button slotBtn;

    private IconInfo iconInfo;
    private int id;

    private void Awake()
    {
        slotBtn.onClick.AddListener(() =>
        {
          //  PlayerManager.Instance.PlayerViewModel.AddItem(id);
        });
    }

    public void Init(int id)
    {
        this.id = id;
        iconInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<IconInfo>(nameof(IconInfo), id);

        UpdateUI();
    }

    public override void UpdateUI()
    {
        IconImg.sprite = Resources.Load<Sprite>(iconInfo.Path);
    }
}
