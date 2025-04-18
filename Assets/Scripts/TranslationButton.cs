using UnityEngine;
using UnityEngine.UI;

public class TranslationButton : MonoBehaviour
{
    [SerializeField] private Button slotBtn;

    private void Start()
    {
        // 게임 매니저 로딩씬으로 빼고 awake로 변경해야 됨
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
