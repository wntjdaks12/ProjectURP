using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameResultItemSlot : View
{
    [SerializeField] private Image itemIconImage;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Transform bg;

    private Sequence sequence;

    private void Awake()
    {
        sequence = DOTween.Sequence()
            .SetAutoKill(false) 
            .Pause()
            .Append(bg.transform.DOScale(new Vector3(1, 1, 1), 0.1f));
    }

    private void OnEnable()
    {
        bg.transform.localScale = new Vector3(0, 3, 1);
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
