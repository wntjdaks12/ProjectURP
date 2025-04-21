using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class MiniHUDObject : UIObject
{
    [SerializeField] private Image currentHpImg;
    [SerializeField] private Image currentMpImg;

    public CharacterObject Target { get; set; }
    public StatAbility StatAbility { get; set; }

    public void Init(CharacterObject target,  StatAbility statAbility)
    {
        Target = target;
        StatAbility = statAbility;

        StatAbility.PropertyChanged -= OnViewModelPropertyChanged;
        StatAbility.PropertyChanged += OnViewModelPropertyChanged;

        target.Character.OnDeathEvent += () => { Entity.OnRemoveData(); };

        UpdateUI();
    }

    private void LateUpdate()
    {
        if (Target != null)
        {
            rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(Target.MiniHUDNode.position);
        }
    }

    public void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "CurrentHp")
        {
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        if (currentHpImg != null) currentHpImg.fillAmount = (float)StatAbility.CurrentHp / StatAbility.MaxHp;
        if (currentMpImg != null) currentMpImg.fillAmount = (float)StatAbility.CurrentMp / StatAbility.MaxMp;
    }
}
