
using System.Collections.Generic;

// ���� ���� ���� ���� 
public class LevelSpawnInfo : Data
{
    public int WorldNumber { get; set; } // ���� �ѹ�
    public int StageNumber { get; set; } // �������� �ѹ�

    public List<SpawnEntityInfo> EntityInfos { get; set; } // ����  ��ƼƼ ����
}

// ��ƼƼ ���� ���� ����
public class SpawnEntityInfo
{
    public int EntityId { get; set; } // ������ ��ƼƼ id
    public int EntityCount { get; set; } // ������ ��ƼƼ ��
}
