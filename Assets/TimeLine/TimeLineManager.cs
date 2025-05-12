using UnityEngine;

public class TimeLineManager : MonoBehaviour
{
    private static TimeLineManager instance;
    public static TimeLineManager Instance { get => instance ??= FindAnyObjectByType<TimeLineManager>(); }

    [field: SerializeField] public CombatTimeLine CombatTimeLine { get; private set; }
    [field: SerializeField] public CharacterMenuTimeLine CharacterMenuTimeLine { get; private set; }
    [field: SerializeField] public CharacterMenuOutTimeLine CharacterMenuOutTimeLine { get; private set; }  
}
