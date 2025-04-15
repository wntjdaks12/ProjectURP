using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : View
{
    [SerializeField] private Image itemImg;
    [SerializeField] protected Button slotBtn;

    private int itemId;
    private IconInfo iconInfo;

    private Sprite iconSprite;

    public virtual void Init(int itemId)
    {
        this.itemId = itemId;

        iconInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<IconInfo>(nameof(IconInfo), itemId);

        iconSprite = Resources.Load<Sprite>(iconInfo.Path);

        UpdateUI();
    }

    public override void UpdateUI()
    {
        itemImg.sprite = iconSprite;
    }
}
