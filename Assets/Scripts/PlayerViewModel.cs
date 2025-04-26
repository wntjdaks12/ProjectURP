using System.Collections.Generic;
using System.Linq;

public class PlayerViewModel : ViewModel
{
    /*
    public int GetCount()
    {
        return itemIds.Length;
    }*/

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

    #region 영웅 관련
    public class HeroData
    {
        public int id;
        public IconInfo heroIconInfo;
        public IconInfo attributeIconInfo;
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
            attributeIconInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<IconInfo>(nameof(IconInfo), 120001 + (int)hero.AttriButeType)
        });;
    }


    #endregion


  /*  #region 아이템 관련

    private int[] itemIds;
    public void AddItem(int itemId)
    {
        if (itemIds.Any(x => x == 0))
        {
            for (int i = 0; i < itemIds.Length; i++)
            {
                if (itemIds[i] == 0)
                {
                    itemIds[i] = itemId; break;
                }
            }

            var item = GameApplication.Instance.GameModel.PresetData.ReturnData<Item>(nameof(Item), itemId);
            heroObject.SetSkill(item.SkillId);
            if(item.UltSkillId != 0) heroObject.SetSkill(item.UltSkillId);

            OnPropertyChanged();
        }
    }

    public void RemoveItem(int itemId)
    {
        for (int i = 0; i < itemIds.Length; i++)
        {
            if (itemIds[i] == itemId)
            {
                itemIds[i] = 0; break;
            }
        }

        OnPropertyChanged();
    }

    public int GetItem(int index)
    {
        return itemIds[index];
    }

    #endregion
  */
    public PlayerViewModel()
    {
        heroIds = new List<int>();
        HeroDatas = new List<HeroData>();

       // itemIds = new int[3];
    }
}
