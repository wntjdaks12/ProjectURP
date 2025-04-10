using UnityEngine;

public class GameModel : MonoBehaviour
{
    // 사전에 정의된 비 휘발성 데이터
    private GameDataContainer presetData;
    public GameDataContainer PresetData
    {
        get => presetData ??= new GameDataContainer();
    }

    // 런타임 중 할당되는 휘발성 데이터
    private GameDataContainer runTimeData;
    public GameDataContainer RunTimeData
    {
        get => runTimeData ??= new GameDataContainer();
    }

    private void Awake()
    {
        // 비 휘발성 데이터 로드 ------------------------------------------------------------------------------------------
        PresetData.LoadData<PrefabInfo>(nameof(PrefabInfo), "JsonDatas/PrefabInfo"); // 프리팹 정보
        PresetData.LoadData<Hero>(nameof(Hero), "JsonDatas/Hero"); // 영웅
        PresetData.LoadData<Monster>(nameof(Monster), "JsonDatas/Monster"); // 몬스터
        PresetData.LoadData<LevelSpawnInfo>(nameof(LevelSpawnInfo), "JsonDatas/LevelSpawnInfo"); // 레벨 스폰 정보
        PresetData.LoadData<StatData>(nameof(StatData), "JsonDatas/Stat"); // 스탯
        PresetData.LoadData<IconInfo>(nameof(IconInfo), "JsonDatas/IconInfo"); // 아이콘 정보
        PresetData.LoadData<Item>(nameof(Item), "JsonDatas/Item"); // 아이템
        PresetData.LoadData<VFX>(nameof(VFX), "JsonDatas/VFX"); // VFX
        PresetData.LoadData<SkillInfo>(nameof(SkillInfo), "JsonDatas/SkillInfo"); // 스킬 정보
        PresetData.LoadData<Skill>(nameof(Skill), "JsonDatas/Skill"); // 스킬 소환 개체 (스킬 발동으로 생성된 독립된 스킬 개체)
        PresetData.LoadData<SummonSkillDetailInfo>(nameof(SummonSkillDetailInfo), "JsonDatas/SummonSkillDetailInfo"); // 스킬 소환 개체 디테일 정보
        PresetData.LoadData<DropItem>(nameof(DropItem), "JsonDatas/DropItem"); // 드랍 아이템 정보
        PresetData.LoadData<DropItemInfo>(nameof(DropItemInfo), "JsonDatas/DropItemInfo"); // 드랍 아이템 정보
        PresetData.LoadData<CurrencyInfo>(nameof(CurrencyInfo), "JsonDatas/CurrencyInfo"); // 재화 정보
        // ----------------------------------------------------------------------------------------------------------------
    }
}
