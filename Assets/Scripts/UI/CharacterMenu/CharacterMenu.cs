using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : View
{
    [SerializeField] private Button exitBtn;

    private ClickSystem clickSystem;

    private GameObject target;

    [SerializeField] private SkillInfoContents skillInfoContents;

    private CharacterMenuViewModel viewModel;
    private void Awake()
    {
        clickSystem = FindObjectOfType<ClickSystem>();

        if (clickSystem)
        {
            clickSystem.OnCliked.AddListener( (gameObj) =>
            {
                target = gameObj;
                // 테스트 하기위해 임시  코드
                Init(gameObj.GetComponentInParent<HeroObject>().Entity.Id);

                OnShow();
            });
        }

        if (exitBtn != null) exitBtn.onClick.AddListener(OnHide);

        gameObject.SetActive(false);
    }

    public void Init(int id)
    {
        viewModel = new CharacterMenuViewModel(id);

        skillInfoContents?.Init(viewModel);

        UpdateUI();
    }

    #region UI 업데이트 관련
    public override void UpdateUI()
    {
    }
    #endregion

}
