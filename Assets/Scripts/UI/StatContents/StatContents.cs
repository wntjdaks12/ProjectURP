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

    #region UI ������Ʈ ����
    public override void UpdateUI()
    {
        // �ʿ��� ���ȸ� �����ֱ� ���� �ϳ��� ���� ����
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
