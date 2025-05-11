using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameResultItemSlot : View
{
    [SerializeField] private Image slotImage;
    [SerializeField] private Image itemIconImage;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemCountText;
    [SerializeField] private Transform bg;
    [SerializeField] private RectTransform shineImageRectTransform;
    [SerializeField] private RectTransform backgroundEffectNode;
    [SerializeField] private RectTransform appearEffectNode;

    private GameObject curBgEffect;
    private GameObject curAppearEffect;

    private Sequence shineSlotSequence;
    private Sequence slotSequence;

    private ChapterViewModel chapterViewModel;
    private int index;

    public RewardItemSlotStyleLibrary rewardItemSlotstyleLibrary;

    private void Awake()
    {    
        shineSlotSequence = SetShineSlot();
    }

    private void OnEnable()
    {
        bg.transform.localScale = Vector3.zero;
    }

    public void Init(int index, ChapterViewModel chapterViewModel)
    {
        this.index = index;
        this.chapterViewModel = chapterViewModel;

        // ��޺� Ʈ����
        switch (chapterViewModel.Items[index].RarityType)
        {
            case Item.RarityTypes.Common: slotSequence = SetBounceStretch(); break;
            case Item.RarityTypes.Rare: slotSequence = SetBounceStretch(); break;
            case Item.RarityTypes.Epic: slotSequence = SetExpansionRecoil(); break;
            case Item.RarityTypes.Legendary: slotSequence = SetBounceStretch(); break;
        }

        slotSequence.Restart();

        UpdateUI();
    }

    public override void UpdateUI()
    {
        var style = rewardItemSlotstyleLibrary.GetStyle(chapterViewModel.Items[index].RarityType);
        if (style != null)
        {
            slotImage.sprite = style.slotImage; // ��޺� ���� �̹��� ����
            itemNameText.color = style.textColor; // ��޺� ������ �̸� �÷� ����

            // ��޺� ���� ��׶��� ����Ʈ ���� [���� ȣ���ϴ°� �ƴ϶� ������Ʈ Ǯ�� X]
            if (curBgEffect != null)
            {
                Destroy(curBgEffect);
            }
            if (style.backgroundEffect && backgroundEffectNode) curBgEffect = Instantiate(style.backgroundEffect, backgroundEffectNode);

            // ��޺� ���� ���� ����Ʈ ���� [���� ȣ���ϴ°� �ƴ϶� ������Ʈ Ǯ�� X]
            if (curAppearEffect != null)
            {
                Destroy(curAppearEffect);
            }
            if (style.appearEffect && appearEffectNode) curAppearEffect = Instantiate(style.appearEffect, appearEffectNode);
        }

        if (itemIconImage) itemIconImage.sprite = Resources.Load<Sprite>(chapterViewModel.RewardItemIconInfos[index].Path); // ������ ������ �̹��� ����
        if (itemNameText) itemNameText.text = chapterViewModel.RewardItemTextInfos[index].NameKr; // ������ �̸� ����
        if (itemCountText) itemCountText.text = chapterViewModel.ChapterRewardInfo.ChapterRewardItemInfos[index].GetRewardCount().ToString(); // ���� ������ ���� ����

    }

    #region Ʈ����
    // ������ ������ �ϴ� Ʈ����
    private Sequence SetShineSlot()
    {
        return DOTween.Sequence()
            .Append(shineImageRectTransform.DOAnchorPos(new Vector2(400, 400), 1f))
            .AppendCallback(() => shineImageRectTransform.anchoredPosition = Vector2.zero)
            .AppendInterval(3f)
            .SetLoops(-1);
    }

    //  ������ �þ��� ���¿��� �����·� ���ư��� Ʈ����
    private Sequence SetBounceStretch()
    {
        bg.transform.localScale = new Vector3(0, 2, 1);

        return DOTween.Sequence()
            .SetAutoKill(false)
            .Pause()
            .Append(bg.transform.DOScale(Vector3.one, 0.1f));
    }

    // ������ Ȯ��� ���¿��� �����·� ���ư��� Ʈ����
    private Sequence SetExpansionRecoil()
    {
        bg.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        return DOTween.Sequence()
            .SetAutoKill(false)
            .Pause()
            .AppendInterval(0.2f)
            .Append(bg.transform.DOScale(Vector3.one, 0.1f));
    }
    #endregion
}
