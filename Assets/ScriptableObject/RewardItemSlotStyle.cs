using UnityEngine;

[CreateAssetMenu(fileName = "RewardItemSlotStyle", menuName = "UI/RewardItemSlotStyle", order = 1)]
public class RewardItemSlotStyle : ScriptableObject
{
    public Item.RarityTypes rarityType;
    public Sprite slotImage;
    public Color textColor;
    public GameObject appearEffect;
    public GameObject backgroundEffect;
}
