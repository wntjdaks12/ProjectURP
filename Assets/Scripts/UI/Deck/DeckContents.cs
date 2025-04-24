using UnityEngine;

public class DeckContents : View
{
    [SerializeField] private DeckSlot deckSlot;
    [SerializeField] private Transform parent;

    private DeckViewModel deckViewModel;

    public void Awake()
    {
        deckViewModel = new DeckViewModel();
    }

    // ���߿� start�� ����� �Ѳ����� ����
    public void Start()
    {
        Init();
    }

    public void Init()
    {
        UpdateUI();
    }

    public override void UpdateUI()
    {
        for (int i = 0; i < deckViewModel.PlayerViewModel.HeroDatas.Count; i++)
        {
            var slot = Instantiate(deckSlot, parent);
            slot.Init(i, deckViewModel);
        }
    }
}
