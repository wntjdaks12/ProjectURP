using UnityEngine;
using UnityEngine.UI;

public class DeckSlot : View
{
    [Header("�̹���")]
    [SerializeField] private Image heroIconImg;
    [SerializeField] private Image attIconImg;

    [Header("��ư")]
    [SerializeField] private Button slotBtn;

    [Header("üũ")]
    [SerializeField] private GameObject checkedCont;

    private int index;
    private DeckViewModel deckViewModel;

    public void Init(int index, DeckViewModel deckViewModel)
    {
        this.index = index;
        this.deckViewModel = deckViewModel;

        UpdateUI();
    }

    public override void UpdateUI()
    {
        var heroData = deckViewModel.PlayerViewModel.HeroDatas[index];
        heroIconImg.sprite = Resources.Load<Sprite>(heroData.heroIconInfo.Path);
        attIconImg.sprite = Resources.Load<Sprite>(heroData.attributeIconInfo.Path);
    }
}