using System.Collections.Generic;
using System.Linq;

public class PlayerViewModel : ViewModel
{
    private int heroId;
    public int HeroId
    {
        get { return heroId; }
        set
        {
            if (heroId != value)
            {
                heroId = value;

                OnPropertyChanged();
            }
        }
    }

    public int GetCount()
    {
        return itemIds.Length;
    }

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

    public void Exp(int exp)
    {
        heroObject.SetExp(exp);

        OnPropertyChanged();
    }


    #region 아이템 관련

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

    public PlayerViewModel()
    {
        itemIds = new int[3];
    }
}
