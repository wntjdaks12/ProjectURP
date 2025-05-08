using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultPanel : View
{
    [SerializeField] private GameResultItemSlot itemSlot;
    [SerializeField] private Transform parent;

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

        // ���� Ǯ�� �̱������� �ӽù������� DestroyImmediate ���
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
