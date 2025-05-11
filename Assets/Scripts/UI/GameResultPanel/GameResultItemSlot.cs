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

        // 등급별 트위닝
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
            slotImage.sprite = style.slotImage; // 등급별 슬롯 이미지 설정
            itemNameText.color = style.textColor; // 등급별 아이템 이름 컬러 설정

            // 등급별 슬롯 백그라운드 이펙트 생성 [자주 호출하는게 아니라서 오브젝트 풀링 X]
            if (curBgEffect != null)
            {
                Destroy(curBgEffect);
            }
            if (style.backgroundEffect && backgroundEffectNode) curBgEffect = Instantiate(style.backgroundEffect, backgroundEffectNode);

            // 등급별 슬롯 출현 이펙트 생성 [자주 호출하는게 아니라서 오브젝트 풀링 X]
            if (curAppearEffect != null)
            {
                Destroy(curAppearEffect);
            }
            if (style.appearEffect && appearEffectNode) curAppearEffect = Instantiate(style.appearEffect, appearEffectNode);
        }

        if (itemIconImage) itemIconImage.sprite = Resources.Load<Sprite>(chapterViewModel.RewardItemIconInfos[index].Path); // 아이템 아이콘 이미지 설정
        if (itemNameText) itemNameText.text = chapterViewModel.RewardItemTextInfos[index].NameKr; // 아이템 이름 설정
        if (itemCountText) itemCountText.text = chapterViewModel.ChapterRewardInfo.ChapterRewardItemInfos[index].GetRewardCount().ToString(); // 보상 아이템 개수 설정

    }

    #region 트위닝
    // 슬롯이 빛나게 하는 트위닝
    private Sequence SetShineSlot()
    {
        return DOTween.Sequence()
            .Append(shineImageRectTransform.DOAnchorPos(new Vector2(400, 400), 1f))
            .AppendCallback(() => shineImageRectTransform.anchoredPosition = Vector2.zero)
            .AppendInterval(3f)
            .SetLoops(-1);
    }

    //  슬롯이 늘어진 상태에서 원상태로 돌아가는 트위닝
    private Sequence SetBounceStretch()
    {
        bg.transform.localScale = new Vector3(0, 2, 1);

        return DOTween.Sequence()
            .SetAutoKill(false)
            .Pause()
            .Append(bg.transform.DOScale(Vector3.one, 0.1f));
    }

    // 슬롯이 확장된 상태에서 원상태로 돌아가는 트위닝
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
