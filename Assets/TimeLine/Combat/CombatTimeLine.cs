using UnityEngine;
using UnityEngine.Playables;

public class CombatTimeLine : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;

    public void Play()
    {
        director.Play();
    }
}
