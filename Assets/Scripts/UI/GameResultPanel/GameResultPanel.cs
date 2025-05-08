using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultPanel : View
{
    [SerializeField] private GameResultItemSlot itemSlot;
    [SerializeField] private Transform parent;

    public void Init()
    {
        UpdateUI();
    }

    public override void UpdateUI()
    {
        StartCoroutine(UpdateSlotAsync());
    }

    public IEnumerator UpdateSlotAsync()
    {
        var slots = new List<GameResultItemSlot>();

        // 슬롯 풀링 미구현으로 임시방편으로 DestroyImmediate 사용
        var lenth = parent.childCount;
        for (int i = 0; i < lenth; i++)
        {
            DestroyImmediate(parent.GetChild((lenth - 1) - i).gameObject);
        }

        for (int i = 0; i < 10; i++)
        {
            slots.Add(Instantiate(itemSlot, parent));
        }

        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].Init();

            yield return new WaitForSeconds(0.05f);
        }
    }
}
