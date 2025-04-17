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

    // 나중에 한 스크립트에서 초기화 관리
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
        // 아이템 수가 적어서 스크롤 풀링 적용 X
        // 추후 수정 필요
        var items = GameApplication.Instance.GameModel.PresetData.ReturnDatas<Item>(nameof(Item));

        for (int i = 0; i < items.Length; i++)
        {
            var slot = Instantiate(itemStoreSlot, parent);
            slot.Init(items[i].Id);
        }
    }
}
