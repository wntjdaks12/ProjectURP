using System.ComponentModel;
using UnityEngine;

public class PlayerHUD : View
{
    public PlayerViewModel ViewModel { get; private set; }

    [SerializeField] private PlayerHUDItemSlot[] itemSlots;
    [SerializeField] private PlayerHUDEXPBar expBar;
    [SerializeField] private StatContents statContents;

    // 나중에 한 스크립트에서 초기화 관리
    private void Start()
    {
        Init();
    }

    public void Init()  
    {
        if (ViewModel == null)
        {
            ViewModel = PlayerManager.Instance.PlayerViewModel;
            ViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }


    }

    public void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "HeroObject")
        {
            UpdateUI();

            statContents.Init(ViewModel);
        }
        else if (e.PropertyName == "Exp")
        {
            UpdateUI();
        }
    }

    public override void UpdateUI()
    {
        var count = ViewModel.GetCount();

        for (int i = 0; i < count; i++)
        {
            var itemId = ViewModel.GetItem(i);

            if(itemId != 0) itemSlots[i].Init(ViewModel.GetItem(i));
        }

        expBar.Init(ViewModel.HeroObject.Hero.CurrentExp, ViewModel.HeroObject.Hero.MaxExp);
    }
}
