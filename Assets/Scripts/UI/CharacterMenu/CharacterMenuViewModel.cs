using System.Linq;
public class CharacterMenuViewModel : ViewModel
{
    private PlayerViewModel playerViewModel;

    private int id;
    public CharacterMenuViewModel(int id)
    {
        playerViewModel = PlayerManager.Instance.PlayerViewModel;

        this.id = id;
    }

    public PlayerViewModel.HeroData GetHeroData()
    {
        return playerViewModel.HeroDatas.Where(x => x.id == id).FirstOrDefault();
    }
}
