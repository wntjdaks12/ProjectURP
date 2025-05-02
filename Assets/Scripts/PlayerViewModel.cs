using System.Collections.Generic;
using System.Linq;

public class PlayerViewModel : ViewModel
{
    private HeroObject heroObject;
    public HeroObject HeroObject 
    {
        get{ return heroObject;}
        set 
        {
            if (heroObject != value)
            {
                heroObject = value;

                OnPropertyChanged();
            }
        }
    }

    #region ¿µ¿õ °ü·Ã
    public class HeroData
    {
        public int id;
        public IconInfo heroIconInfo;
        public IconInfo attributeIconInfo;
        public IconInfo skillIconInfo;
        public TextInfo skillTextInfo;
    }

    private List<int> heroIds;
    public List<HeroData> HeroDatas { get; private set; }

    public void AddHero(int heroId)
    {
        var hero = GameApplication.Instance.GameModel.PresetData.ReturnData<Hero>(nameof(Hero), heroId);

        heroIds.Add(heroId);

        HeroDatas.Add(new HeroData
        {
            id = heroId,
            heroIconInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<IconInfo>(nameof(IconInfo), heroId),
            attributeIconInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<IconInfo>(nameof(IconInfo), 120001 + (int)hero.AttriButeType),
            skillIconInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<IconInfo>(nameof(IconInfo), hero.SkillId),
            skillTextInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<TextInfo>(nameof(TextInfo), hero.SkillId)
        });;
    }


    #endregion

    public PlayerViewModel()
    {
        heroIds = new List<int>();
        HeroDatas = new List<HeroData>();
    }
}
