using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : View
{
    [SerializeField] private Button exitBtn;

    private ClickSystem clickSystem;

    [SerializeField] private SkillInfoContents skillInfoContents;
    [SerializeField] private StatContents statContents;

    private CharacterMenuViewModel viewModel;
    private void Awake()
    {
        clickSystem = FindObjectOfType<ClickSystem>();

        if (clickSystem)
        {
            clickSystem.OnCliked.AddListener( (gameObj) =>
            {
                CharacterObject characterObject;

                if (gameObj.transform.parent.TryGetComponent(out characterObject))
                {
                    Init(characterObject);

                    OnShow();
                }
            });
        }

        if (exitBtn != null) exitBtn.onClick.AddListener(OnHide);

        gameObject.SetActive(false);
    }
    public void Init(CharacterObject characterObject)
    { 
        viewModel = new CharacterMenuViewModel(characterObject);

        skillInfoContents?.Init(viewModel);
        statContents?.Init(viewModel);

        UpdateUI();
    }

    #region UI 업데이트 관련
    public override void UpdateUI()
    {
    }
    #endregion

}
