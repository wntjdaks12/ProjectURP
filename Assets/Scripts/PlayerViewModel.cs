using System.Collections.Generic;

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

    private int[] itemIds;
    public void AddItem(int itemId)
    {
        for (int i = 0; i < itemIds.Length; i++)
        {
            if (itemIds[i] == 0)
            {
                itemIds[i] = itemId; break;
            }
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
    }

    public int GetItem(int index)
    {
        return itemIds[index];
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

    public PlayerViewModel()
    {
        itemIds = new int[3];
    }
}
