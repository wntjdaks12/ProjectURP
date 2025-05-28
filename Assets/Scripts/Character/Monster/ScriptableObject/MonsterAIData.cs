using UnityEngine;

[CreateAssetMenu(fileName = "MonsterAIData", menuName = "Game/Monster/AI")]
public class MonsterAIData : ScriptableObject
{
    [field: SerializeField] public float minSateTransitionTime { get; private set;} // 상태 최소 변환 시간
    [field: SerializeField] public float maxStateTransitionTime { get; private set; } // 상태 최대 변환 시간
    [field: SerializeField] public float insideUnitSphere { get; private set; } // 이동 시 스폰 위치 기준으로 구의 임의의 위치 값
}
