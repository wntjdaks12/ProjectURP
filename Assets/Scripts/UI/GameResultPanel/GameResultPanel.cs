using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultPanel : View
{
    [SerializeField] private GameResultItemSlot itemSlot;
    [SerializeField] private Transform parent;

    private ChapterViewModel chapterViewModel;

    private void Start()
    {
        chapterViewModel = ChapterManager.Instance.ChapterViewModel;
    }

    public void Init()
    {
        OnShow();

        UpdateUI();
    }

    public override void UpdateUI()
    {
    }

    public void UpdateSlot()
    {
        StartCoroutine(UpdateSlotAsync());
    }

    private IEnumerator UpdateSlotAsync()
    {
        var slots = new List<GameResultItemSlot>();

        // 슬롯 풀링 미구현으로 임시방편으로 DestroyImmediate 사용
        var lenth = parent.childCount;
        for (int i = 0; i < lenth; i++)
        {
            DestroyImmediate(parent.GetChild((lenth - 1) - i).gameObject);
        }

        if (chapterViewModel.ChapterRewardInfo != null && chapterViewModel.ChapterRewardInfo.ChapterRewardItemInfos != null)
        {
            for (int i = 0; i < chapterViewModel.ChapterRewardInfo.ChapterRewardItemInfos.Count; i++)
            {
                slots.Add(Instantiate(itemSlot, parent));
            }

            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].Init(i, chapterViewModel);

                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
