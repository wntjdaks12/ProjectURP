using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class DeckContents : View
{
    [SerializeField] private DeckSlot deckSlot;
    [SerializeField] private Transform parent;


    [Header("전투")]
    [SerializeField] private Button combatBtn;
    [SerializeField] private Image combatLockImg;

    private DeckViewModel deckViewModel;

    public void Awake()
    {
        deckViewModel = new DeckViewModel();

        combatBtn.onClick.AddListener(() =>
        {

        });

        deckViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    // 나중에 start문 지우고 한꺼번에 관리
    public void Start()
    {
        Init();
    }

    public void Init()
    {
        UpdateUI();
    }

    public void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "AddCombatDeck")
        {
            UpdateCombatButton();
        }
        else if (e.PropertyName == "RemoveCombatDeck")
        {
            UpdateCombatButton();
        }
    }

    #region UI 업데이트 관련
    public override void UpdateUI()
    {
        for (int i = 0; i < deckViewModel.PlayerViewModel.HeroDatas.Count; i++)
        {
            var slot = Instantiate(deckSlot, parent);
            slot.Init(i, deckViewModel);
        }

        UpdateCombatButton();
    }

    private void UpdateCombatButton()
    {
        if(deckViewModel.CombatCount > 0)   
            combatLockImg.gameObject.SetActive(false);
        else
            combatLockImg.gameObject.SetActive(true);
    }
    #endregion
}
