using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDEXPBar : MonoBehaviour
{
    [SerializeField] private Image currentExpImg;

    public void Init(int curExp, int MaxExp)
    {
        currentExpImg.fillAmount = (float)curExp / MaxExp;
    }
}
