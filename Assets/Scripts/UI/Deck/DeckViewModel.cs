using System.Collections.Generic;

public class DeckViewModel : ViewModel
{
    public PlayerViewModel PlayerViewModel { get; private set; }

    public DeckViewModel()
    {
        PlayerViewModel = PlayerManager.Instance.PlayerViewModel;
    }
}
