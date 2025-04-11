using System;
using UnityEngine;
using TMPro;

public class DamagePopupObject : UIObject
{
    [SerializeField] private TextMeshProUGUI damageText;

    public override void Init(Entity entity)
    {
        base.Init(entity);

        damageText.text = "";
    }

    public void UpdteUI(int damage)
    {
        damageText.text = damage.ToString();
    }
}
