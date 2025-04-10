
using System.Collections.Generic;

// 레벨 스폰 관련 정보 
public class LevelSpawnInfo : Data
{
    public int WorldNumber { get; set; } // 월드 넘버
    public int StageNumber { get; set; } // 스테이지 넘버

    public List<SpawnEntityInfo> EntityInfos { get; set; } // 스폰  엔티티 정보
}

// 엔티티 스폰 관련 정보
public class SpawnEntityInfo
{
    public int EntityId { get; set; } // 스폰할 엔티티 id
    public int EntityCount { get; set; } // 스폰할 엔티티 수
}
