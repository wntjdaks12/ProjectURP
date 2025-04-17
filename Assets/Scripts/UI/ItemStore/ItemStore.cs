using UnityEngine;

public class ItemStore : View
{
    [SerializeField] private ItemStoreSlot itemStoreSlot;
    [SerializeField] private Transform parent;

    private ItemStoreViewModel viewModel;

    private void Awake()
    {
        viewModel = new ItemStoreViewModel();
    }

    // ���߿� �� ��ũ��Ʈ���� �ʱ�ȭ ����
    private void Start()
    {
        Init();
    }

    public void Init()
    {
        UpdateUI();
    }

    public override void UpdateUI()
    {
        // ������ ���� ��� ��ũ�� Ǯ�� ���� X
        // ���� ���� �ʿ�
        var items = GameApplication.Instance.GameModel.PresetData.ReturnDatas<Item>(nameof(Item));

        for (int i = 0; i < items.Length; i++)
        {
            var slot = Instantiate(itemStoreSlot, parent);
            slot.Init(items[i].Id);
        }
    }
}
