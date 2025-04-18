using UnityEngine;
using UnityEngine.UI;

public class TranslationButton : MonoBehaviour
{
    [SerializeField] private Button slotBtn;

    private void Awake()
    {
        slotBtn.onClick.AddListener(() =>
        {
            switch (GameManager.Instance.LanguageType)
            {
                case GameManager.LanguageTypes.En: GameManager.Instance.SetLanguage(GameManager.LanguageTypes.Kr); break;
                case GameManager.LanguageTypes.Kr: GameManager.Instance.SetLanguage(GameManager.LanguageTypes.En); break;
            }
        });
    }
}
