using UnityEngine;

public class TimeLineManager : MonoBehaviour
{
    private static TimeLineManager instance;
    public static TimeLineManager Instance { get => instance ??= FindAnyObjectByType<TimeLineManager>(); }

    [field: SerializeField] public UltSkillEffectTimeLineDirector UltSkillEffectTimeLineDirector { get; private set; }
    [field: SerializeField] public CombatTimeLine CombatTimeLine { get; private set; }

}
