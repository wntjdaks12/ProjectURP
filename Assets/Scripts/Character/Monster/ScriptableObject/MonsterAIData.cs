using UnityEngine;

[CreateAssetMenu(fileName = "MonsterAIData", menuName = "Game/Monster/AI")]
public class MonsterAIData : ScriptableObject
{
    [field: SerializeField] public float minSateTransitionTime { get; private set;} // ���� �ּ� ��ȯ �ð�
    [field: SerializeField] public float maxStateTransitionTime { get; private set; } // ���� �ִ� ��ȯ �ð�
    [field: SerializeField] public float insideUnitSphere { get; private set; } // �̵� �� ���� ��ġ �������� ���� ������ ��ġ ��
}
