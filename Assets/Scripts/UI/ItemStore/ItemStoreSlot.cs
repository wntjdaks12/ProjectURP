using UnityEngine.UI;
using System;
using UnityEngine;

public class ItemStoreSlot : View
{
    [SerializeField] private Image IconImg;
    [SerializeField] private Button slotBtn;

    private IconInfo iconInfo;

    private void Awake()
    {
        slotBtn.onClick.AddListener(() =>
        {

        });
    }

    public void Init(int id)
    {
        iconInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<IconInfo>(nameof(IconInfo), id);

        UpdateUI();
    }

    public override void UpdateUI()
    {
        IconImg.sprite = Resources.Load<Sprite>(iconInfo.Path);
    }
}
