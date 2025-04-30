using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : View
{
    [SerializeField] private Button exitBtn;
    [SerializeField] private Button skillBtn;

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

        if(exitBtn != null) exitBtn.onClick.AddListener(OnHide);

        OnHide();
    }

    private void LateUpdate()
    {
        if(target != null) UpdatePosition();
    }

    #region UI ������Ʈ ����
    public override void UpdateUI()
    {
        if (target != null) UpdatePosition();
    }

    private void UpdatePosition()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.transform.position);
    }
    #endregion

}
