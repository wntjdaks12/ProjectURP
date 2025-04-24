using UnityEngine;
using UnityEngine.UI;

public class DeckSlot : View
{
    [Header("이미지")]
    [SerializeField] private Image heroIconImg;
    [SerializeField] private Image attIconImg;

    [Header("버튼")]
    [SerializeField] private Button slotBtn;

    [Header("오브젝트")]
    [SerializeField] private GameObject checkedCont;

    private int index;
    private DeckViewModel deckViewModel;

    private bool isChecked;

    private void Awake()
    {
        slotBtn.onClick.AddListener(() =>
        {
            if(!isChecked)
                checkedCont.gameObject.SetActive(true);
            else
                checkedCont.gameObject.SetActive(false);

            isChecked = !isChecked;
        });
    }

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