using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : View
{
    [SerializeField] private Button exitBtn;

    private ClickSystem clickSystem;

    private GameObject target;

    private void Awake()
    {
        clickSystem = FindObjectOfType<ClickSystem>();

        if (clickSystem)
        {
            clickSystem.OnCliked.AddListener( (gameObj) =>
            {
                target = gameObj;

                UpdateUI();

                OnShow();
            });
        }

        if (exitBtn != null) exitBtn.onClick.AddListener(OnHide);

        gameObject.SetActive(false);
    }

    #region UI 업데이트 관련
    public override void UpdateUI()
    {
    }
    #endregion

}
