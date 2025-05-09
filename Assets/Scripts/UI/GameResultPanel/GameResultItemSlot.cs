using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameResultItemSlot : View
{
    [SerializeField] private Image itemIconImage;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Transform bg;
    [SerializeField] private RectTransform shineImageRectTransform;

    private Sequence sequence;

    private void Awake()
    {
        sequence = DOTween.Sequence()
            .SetAutoKill(false) 
            .Pause()
            .Append(bg.transform.DOScale(new Vector3(1, 1, 1), 0.1f));

        DOTween.Sequence()
            .Append(shineImageRectTransform.DOAnchorPos(new Vector2(400, 400), 1f))
            .AppendCallback(() => shineImageRectTransform.anchoredPosition = Vector2.zero)
            .AppendInterval(3f)
            .SetLoops(-1);
    }

    private void OnEnable()
    {
        bg.transform.localScale = new Vector3(0, 2, 1);
    }

    public void Init()
    {
        sequence.Restart(); 
    }

    public override void UpdateUI()
    {
        if (itemIconImage) itemIconImage.sprite = null;
        if (itemNameText) itemNameText.text = "null";
    }
}
