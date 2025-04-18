using UnityEngine;
using UnityEngine.UI;

public class TranslationButton : MonoBehaviour
{
    [SerializeField] private Button slotBtn;

    private void Start()
    {
        // ���� �Ŵ��� �ε������� ���� awake�� �����ؾ� ��
        var gameViewModel = GameManager.Instance.GameViewModel;

        slotBtn.onClick.AddListener(() =>
        {
            switch (gameViewModel.LanguageType)
            {
                case GameViewModel.LanguageTypes.En: gameViewModel.SetLanguage(GameViewModel.LanguageTypes.Kr); break;
                case GameViewModel.LanguageTypes.Kr: gameViewModel.SetLanguage(GameViewModel.LanguageTypes.En); break;
            }
        });
    }
}
