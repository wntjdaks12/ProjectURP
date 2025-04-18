using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TooltipBoxModel
{ 
    public TextInfo TextInfo { get; set; }
}

public class TooltipBoxPresenter
{
    private TooltipBoxModel model;
    private ITooltipBoxView view;

    public TooltipBoxPresenter(ITooltipBoxView view)
    {
        this.view = view;
        model = new TooltipBoxModel();
    }

    public void Init(TextInfo TextInfo)
    {
        model.TextInfo = TextInfo;

        view.UpdateUI(model);
    }
}

public interface ITooltipBoxView
{
    public void UpdateUI(TooltipBoxModel model);
}

public class TooltipBox : View
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private RectTransform rectTransform;

    private TextInfo textInfo;

    private Vector2 screenUIOffset;

    public void Init(TextInfo textInfo, Vector2 position, Transform parent)
    {
        this.textInfo = textInfo;
  
        transform.SetParent(parent, false);

        screenUIOffset = screenUIOffset = UIManager.Instance.OverrayCanvas.CanvasScaler.GetScreenUIOffset();

        if (position.x - rectTransform.rect.width < 0)
        {
            rectTransform.anchoredPosition = new Vector2(position.x / screenUIOffset.x, position.y / screenUIOffset.y);
        }
        else if (position.x + rectTransform.rect.width > Screen.width)
        {
            rectTransform.anchoredPosition = new Vector2(position.x / screenUIOffset.x - rectTransform.rect.width, position.y / screenUIOffset.y);
        }
        else
        {
            rectTransform.anchoredPosition = new Vector2(position.x  / screenUIOffset.x,  position.y / screenUIOffset.y);
        }

        UpdateUI();
    }

    public override void UpdateUI()
    {
        if (textInfo != null)
        {
            switch (GameManager.Instance.LanguageType)
            {
                case GameManager.LanguageTypes.Kr:
                    nameText.text = textInfo.NameKr;
                    descriptionText.text = textInfo.DescriptionKr;
                    break;
                case GameManager.LanguageTypes.En:
                    nameText.text = textInfo.NameEn;
                    descriptionText.text = textInfo.DescriptionEn;
                    break;
            }
        }
        else
        {
            nameText.text = "null";
            descriptionText.text = "null";
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
    }


}
    