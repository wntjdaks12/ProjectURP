using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDEXPBar : View
{
    [SerializeField] private Image currentExpImg;

    private int curExp;
    private int maxExp;

    public void Init(int curExp, int maxExp)
    {
        this.curExp = curExp;
        this.maxExp = maxExp;

        UpdateUI();
    }

    public override void UpdateUI()
    {
        currentExpImg.fillAmount = (float)curExp / maxExp;
    }
}
