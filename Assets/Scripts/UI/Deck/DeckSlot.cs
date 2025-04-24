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
    private int heroId;

    private void Awake()
    {
        slotBtn.onClick.AddListener(() =>
        {
            if (!isChecked)
            {
                checkedCont.gameObject.SetActive(true);

                var heroData = deckViewModel.PlayerViewModel.HeroDatas[index];
                var heroObj = GameApplication.Instance.EntityController.Spawn<Hero, HeroObject>(heroData.id, new Vector3(0, 6.937001f, 0), Quaternion.identity);

                GameApplication.Instance.EntityController.Spawn<VFX, VFXObject>(40005, heroObj.transform.position, Quaternion.identity);

                deckViewModel.AddCombatDeck(heroObj.Id);
                heroId = heroObj.Id;
            }
            else
            {
                checkedCont.gameObject.SetActive(false);

                var hero = GameApplication.Instance.GameModel.RunTimeData.ReturnData<Hero>(nameof(Hero), heroId);
                hero.OnRemoveData();

                deckViewModel.RemoveCombatDeck(heroId);
                heroId = 0;
            }

            isChecked = !isChecked;
        });
    }

    public void Init(int index, DeckViewModel deckViewModel)
    {
        this.index = index;
        this.deckViewModel = deckViewModel;

        isChecked = false;
        heroId = 0;

        UpdateUI();
    }

    public override void UpdateUI()
    {
        var heroData = deckViewModel.PlayerViewModel.HeroDatas[index];
        heroIconImg.sprite = Resources.Load<Sprite>(heroData.heroIconInfo.Path);
        attIconImg.sprite = Resources.Load<Sprite>(heroData.attributeIconInfo.Path);
    }
}