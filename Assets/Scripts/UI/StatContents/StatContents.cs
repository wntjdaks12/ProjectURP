using UnityEngine;

public class StatContents : View
{
    [SerializeField] private StatSlot statSlot;
    [SerializeField] private Transform parent;

    private CharacterMenuViewModel viewModel;

    public void Init(CharacterMenuViewModel viewModel)
    {
        this.viewModel = viewModel;

        UpdateUI();
    }

    #region UI 업데이트 관련
    public override void UpdateUI()
    {
        // 필요한 스탯만 보여주기 위해 하나씩 슬롯 생성
        var slot1 = Instantiate(statSlot, parent);
        slot1.Init(viewModel, Stat.StatTypes.MaxHp);

        var slot2 = Instantiate(statSlot, parent);
        slot2.Init(viewModel, Stat.StatTypes.AttackDamage);

        var slot3 = Instantiate(statSlot, parent);
        slot3.Init(viewModel, Stat.StatTypes.AbilityPower);

        var slot4 = Instantiate(statSlot, parent);
        slot4.Init(viewModel, Stat.StatTypes.MaxSpeed);
    }
    #endregion
}
