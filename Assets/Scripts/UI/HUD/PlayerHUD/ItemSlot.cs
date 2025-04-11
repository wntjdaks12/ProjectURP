using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image itemImg;

    private IconInfo iconInfo;

    private Sprite iconSprite;

    public virtual void Init(int itemId)
    {
        iconInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<IconInfo>(nameof(IconInfo), itemId);

        iconSprite = Resources.Load<Sprite>(iconInfo.Path);

        UpdateUI();
    }

    public virtual void UpdateUI()
    {
        itemImg.sprite = iconSprite;
    }
}
