using System.Collections.Generic;

public class DeckViewModel : ViewModel
{
    public PlayerViewModel PlayerViewModel { get; private set; }

    private List<int> combatDecks;

    public DeckViewModel()
    {
        PlayerViewModel = PlayerManager.Instance.PlayerViewModel;

        combatDecks = new List<int>();
    }

    #region ��Ʋ ����
    public void AddCombatDeck(int heroId)
    {
        combatDecks.Add(heroId);

        OnPropertyChanged();
    }

    public void RemoveCombatDeck(int heroId)
    {
        combatDecks.Remove(heroId);

        OnPropertyChanged();
    }

    public int CombatCount { get { return combatDecks.Count; } }
    #endregion
}
