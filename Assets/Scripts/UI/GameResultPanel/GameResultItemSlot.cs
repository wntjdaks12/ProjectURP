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

    [SerializeField] private GameObject commonSlot;
    [SerializeField] private GameObject rareSlot;
    [SerializeField] private GameObject epicSlot;
    [SerializeField] private GameObject legendarySlot;

    [SerializeField] private GameObject commonEff;
    [SerializeField] private GameObject rareEff;
    [SerializeField] private GameObject epicEff;
    [SerializeField] private GameObject legendaryEff;

    private Sequence shineSlotSequence;
    private Sequence slotSequence;

    private ChapterViewModel chapterViewModel;
    private int index;

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
        if (itemIconImage) itemIconImage.sprite = Resources.Load<Sprite>(chapterViewModel.RewardItemIconInfos[index].Path);
        if (itemNameText) itemNameText.text = chapterViewModel.RewardItemTextInfos[index].NameKr;

        commonSlot.SetActive(false);
        rareSlot.SetActive(false);
        epicSlot.SetActive(false);
        legendarySlot.SetActive(false);

        commonEff.SetActive(false);
        rareEff.SetActive(false);
        epicEff.SetActive(false);
        legendaryEff.SetActive(false);

        switch (chapterViewModel.Items[index].RarityType)
        {
            case Item.RarityTypes.Common: commonSlot.SetActive(true); commonEff.SetActive(true); break;
            case Item.RarityTypes.Rare: rareSlot.SetActive(true); rareEff.SetActive(true); break;
            case Item.RarityTypes.Epic: epicSlot.SetActive(true); epicEff.SetActive(true); break;
            case Item.RarityTypes.Legendary: legendarySlot.SetActive(true); legendaryEff.SetActive(true); break;
        }
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
