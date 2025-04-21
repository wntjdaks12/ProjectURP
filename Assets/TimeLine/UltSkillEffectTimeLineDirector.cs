using UnityEngine;
using UnityEngine.Playables;

public class UltSkillEffectTimeLineDirector : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;

    public void Play()
    {
        director.Play();
    }
}
