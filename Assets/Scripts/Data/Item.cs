public class Item : Data
{
    public enum RarityTypes
    {
        Common = 0,
        Rare = 1,
        Epic = 2,
        Legendary = 3
    }

    public RarityTypes RarityType { get; set; }
}
