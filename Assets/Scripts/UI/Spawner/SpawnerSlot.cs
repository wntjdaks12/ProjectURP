using TMPro;
using UnityEngine;

public class SpawnerSlot : View
{
    [SerializeField] private TextMeshProUGUI wordsText;

    private string str;

    public void Init(string str)
    {
        this.str = str;

        UpdateUI();
    }

    public override void UpdateUI()
    {
        wordsText.text = str + "��/�� ����߽��ϴ�.";
    }
}
