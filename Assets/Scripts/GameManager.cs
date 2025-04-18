using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public enum LanguageTypes
    {
        Kr,
        En
    }

    public LanguageTypes LanguageType { get; private set; }

    private static GameManager instance;
    public static GameManager Instance { get => instance ??= FindAnyObjectByType<GameManager>(); }

    private void Start()
    {
        var palyerManager = PlayerManager.Instance;
        palyerManager.PlayerViewModel.HeroObject = GameApplication.Instance.EntityController.Spawn<Hero, HeroObject>(palyerManager.PlayerViewModel.HeroId, Vector3.zero, Quaternion.identity);

        LanguageType = LanguageTypes.Kr;
    }

    public void SetLanguage(LanguageTypes languageType)
    {
        LanguageType = languageType;
    }
}
