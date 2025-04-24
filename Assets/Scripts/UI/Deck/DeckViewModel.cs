public class DeckViewModel : ViewModel
{
    public DeckViewModel()
    {
        PlayerViewModel = PlayerManager.Instance.PlayerViewModel;
    }

    public PlayerViewModel PlayerViewModel { get; private set; }
}
