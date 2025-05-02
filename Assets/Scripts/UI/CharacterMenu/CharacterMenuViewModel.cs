using System.Linq;
public class CharacterMenuViewModel : ViewModel
{
    private PlayerViewModel playerViewModel;

    public CharacterObject CharacterObject { get; private set; }
    public CharacterMenuViewModel(CharacterObject characterObject)
    {
        playerViewModel = PlayerManager.Instance.PlayerViewModel;

        CharacterObject = characterObject;
    }

    public PlayerViewModel.HeroData GetHeroData()
    {
        return playerViewModel.HeroDatas.Where(x => x.id == CharacterObject.Entity.Id).FirstOrDefault();
    }
}
