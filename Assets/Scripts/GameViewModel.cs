using UnityEngine;

public class GameViewModel : ViewModel
{
    #region 언어 관련
    public enum LanguageTypes
    {
        Kr,
        En
    }

    public LanguageTypes LanguageType { get; private set; }

    public void SetLanguage(LanguageTypes languageType)
    {
        LanguageType = languageType;

        OnPropertyChanged();
    }
    #endregion
}
